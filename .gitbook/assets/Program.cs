using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
// File: Program.cs
//
// This console application demonstrates various CRUD operations on a MongoDB/DocumentDB
// instance using the recommended multi-tenant and multi-hierarchical schema.
//
// To run this, you need to install the MongoDB.Driver NuGet package:
// dotnet add package MongoDB.Driver
//
// IMPORTANT: Replace "YOUR_CONNECTION_STRING" and "YOUR_TENANT_ID" with your actual values.
//



namespace TaskManagerApp
{
    // Define the data models based on the provided schema.
    // The BsonId attribute maps the C# Id property to the MongoDB _id field.
    public class TaskDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string tenantId { get; set; }
        public string taskId { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime createdDate { get; set; }
        public string parentId { get; set; }
        public List<string> ancestors { get; set; }
        public string path { get; set; }
        public int level { get; set; }
        public bool hasChildren { get; set; }
        public int childrenCount { get; set; }
        public Dictionary<string, CustomField> customFields { get; set; }
        public Denormalized denormalized { get; set; }
        public Metadata metadata { get; set; }
        public string searchText { get; set; }
    }

    public class CustomField
    {
        public BsonValue value { get; set; }
        public string type { get; set; }
        public string displayName { get; set; }
    }

    public class Denormalized
    {
        public string rootTaskId { get; set; }
        public string rootTaskTitle { get; set; }
        public string parentTitle { get; set; }
        public List<Breadcrumb> breadcrumb { get; set; }
    }

    public class Breadcrumb
    {
        public string id { get; set; }
        public string title { get; set; }
    }

    public class Metadata
    {
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public int version { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
    }

    public class TaskService
    {
        private readonly IMongoCollection<TaskDocument> _tasks;
        private const string TenantId = "tenant_001";

        public TaskService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _tasks = database.GetCollection<TaskDocument>("tasksInstance-1");
        }

        // 1. Create/Insert
        public async Task InsertHierarchy(int parents, int children, int levels)
        {
            Console.WriteLine($"\n--- Inserting {parents} parents, {children} children, and {levels} levels. ---\n");

            for (int i = 0; i < parents; i++)
            {
                // Create a parent task (level 0)
                var parentTaskId = $"TASK-{Guid.NewGuid()}";
                var parentTask = CreateTask(parentTaskId, null, new List<string>(), 0, $"Parent Task {i + 1}");
                await _tasks.InsertOneAsync(parentTask);
                Console.WriteLine($"Inserted parent task: {parentTask.title} with ID: {parentTaskId}");

                await InsertChildren(parentTask, children, 1, levels);
            }
            Console.WriteLine("\n--- Insert operation complete. ---\n");
        }

        private async Task InsertChildren(TaskDocument parent, int count, int currentLevel, int maxLevels)
        {
            if (currentLevel > maxLevels)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var taskId = $"TASK-{Guid.NewGuid()}";
                var ancestors = new List<string>(parent.ancestors) { parent.taskId };
                var childTask = CreateTask(taskId, parent.taskId, ancestors, currentLevel, $"{parent.title}'s Child {i + 1}");

                await _tasks.InsertOneAsync(childTask);
                Console.WriteLine($"  Inserted child task: {childTask.title} with ID: {taskId} at level {currentLevel}");

                // Recursively insert sub-children
                await InsertChildren(childTask, count, currentLevel + 1, maxLevels);
            }
        }

        private TaskDocument CreateTask(string taskId, string parentId, List<string> ancestors, int level, string title)
        {
            var random = new Random();
            var status = random.Next(3) switch
            {
                0 => "in_progress",
                1 => "completed",
                _ => "active"
            };
            var priority = random.Next(3) switch
            {
                0 => "low",
                1 => "medium",
                _ => "high"
            };

            var task = new TaskDocument
            {
                tenantId = TenantId,
                taskId = taskId,
                title = title,
                status = status,
                priority = priority,
                dueDate = DateTime.UtcNow.AddDays(random.Next(30, 90)),
                createdDate = DateTime.UtcNow,
                parentId = parentId,
                ancestors = ancestors,
                path = parentId != null ? $"{string.Join("/", ancestors)}/{taskId}" : $"/{taskId}",
                level = level,
                hasChildren = true, // We'll assume a new task can have children for the sake of the demo
                childrenCount = 0,
                customFields = new Dictionary<string, CustomField>
                {
                    { "estimatedHours", new CustomField { value = new BsonInt32(random.Next(1, 50)), type = "number", displayName = "Estimated Hours" } },
                    { "department", new CustomField { value = new BsonString("Engineering"), type = "string", displayName = "Department" } },
                    { "assignee", new CustomField { value = new BsonString($"developer{random.Next(1, 10)}@company.com"), type = "string", displayName = "Assignee" } }
                },
                denormalized = new Denormalized
                {
                    rootTaskId = ancestors.Count > 0 ? ancestors[0] : taskId,
                    rootTaskTitle = "Project Root",
                    parentTitle = "Parent Title"
                },
                metadata = new Metadata
                {
                    createdBy = "system",
                    modifiedBy = "system",
                    modifiedDate = DateTime.UtcNow,
                    version = 1
                },
                searchText = $"{title} {status} {priority}"
            };
            return task;
        }

        // 2. Single Update
        public async Task UpdateSingleTask(string taskIdToUpdate)
        {
            Console.WriteLine($"\n--- Updating single task with ID: {taskIdToUpdate} ---\n");
            var filter = Builders<TaskDocument>.Filter.Eq(t => t.taskId, taskIdToUpdate);
            var update = Builders<TaskDocument>.Update
                .Set(t => t.status, "completed")
                .Set(t => t.metadata.modifiedDate, DateTime.UtcNow)
                .Set(t => t.customFields["estimatedHours"].value, new BsonInt32(new Random().Next(51, 100)));

            var result = await _tasks.UpdateOneAsync(filter, update);

            Console.WriteLine(result.ModifiedCount > 0 ? "Update successful." : "Update failed. Task not found.");
            Console.WriteLine("\n--- Update operation complete. ---\n");
        }

        // 3. Delete
        public async Task DeleteTask(string taskIdToDelete)
        {
            Console.WriteLine($"\n--- Deleting task with ID: {taskIdToDelete} and its children ---\n");

            // Find all descendants using the ancestors array
            var descendantsFilter = Builders<TaskDocument>.Filter.Eq("ancestors", taskIdToDelete);
            var descendantsToDelete = await _tasks.Find(descendantsFilter).Project(t => t.taskId).ToListAsync();

            if (descendantsToDelete.Count > 0)
            {
                var descendantIdsFilter = Builders<TaskDocument>.Filter.In(t => t.taskId, descendantsToDelete);
                await _tasks.DeleteManyAsync(descendantIdsFilter);
                Console.WriteLine($"Deleted {descendantsToDelete.Count} descendant tasks.");
            }

            // Delete the parent task itself
            var parentFilter = Builders<TaskDocument>.Filter.Eq(t => t.taskId, taskIdToDelete);
            var deleteResult = await _tasks.DeleteOneAsync(parentFilter);

            Console.WriteLine(deleteResult.DeletedCount > 0 ? "Deletion successful." : "Deletion failed. Task not found.");
            Console.WriteLine("\n--- Delete operation complete. ---\n");
        }

        // 4. Read Single
        public async Task ReadSingleTask(string taskIdToRead)
        {
            Console.WriteLine($"\n--- Reading single task with ID: {taskIdToRead} ---\n");
            var filter = Builders<TaskDocument>.Filter.Eq(t => t.taskId, taskIdToRead);
            var task = await _tasks.Find(filter).FirstOrDefaultAsync();

            if (task != null)
            {
                Console.WriteLine($"Found Task: {task.title}");
                Console.WriteLine($"Status: {task.status}, Priority: {task.priority}");
                Console.WriteLine($"Path: {task.path}, Level: {task.level}");
                Console.WriteLine($"Estimated Hours: {task.customFields["estimatedHours"].value}");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
            Console.WriteLine("\n--- Read single operation complete. ---\n");
        }

        // 5. Read Multiple with Range
        public async Task ReadMultipleTasksByRange()
        {
            Console.WriteLine("\n--- Reading multiple tasks with a due date range ---\n");
            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow.AddDays(45);

            var filter = Builders<TaskDocument>.Filter.And(
                Builders<TaskDocument>.Filter.Gte(t => t.dueDate, startDate),
                Builders<TaskDocument>.Filter.Lte(t => t.dueDate, endDate)
            );

            var tasks = await _tasks.Find(filter).ToListAsync();

            Console.WriteLine($"Found {tasks.Count} tasks due between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"- {task.title} (ID: {task.taskId}, Due: {task.dueDate.ToShortDateString()})");
            }
            Console.WriteLine("\n--- Read multiple operation complete. ---\n");
        }

        // 6. Complex Filter
        public async Task ComplexFilterQuery()
        {
            Console.WriteLine("\n--- Performing complex filter query on custom fields ---\n");
            var filter = Builders<TaskDocument>.Filter.And(
                Builders<TaskDocument>.Filter.Eq("customFields.department.value", "Engineering"),
                Builders<TaskDocument>.Filter.Gte("customFields.estimatedHours.value", new BsonInt32(20)),
                Builders<TaskDocument>.Filter.Eq("priority", "high")
            );

            var tasks = await _tasks.Find(filter).ToListAsync();

            Console.WriteLine($"Found {tasks.Count} high-priority Engineering tasks with estimated hours >= 20:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"- {task.title} (ID: {task.taskId})");
            }
            Console.WriteLine("\n--- Complex filter operation complete. ---\n");
        }

        // 7. Multiple Update
        public async Task UpdateMultipleTasks()
        {
            Console.WriteLine("\n--- Updating multiple tasks matching a filter ---\n");
            var filter = Builders<TaskDocument>.Filter.Eq(t => t.status, "in_progress");
            var update = Builders<TaskDocument>.Update
                .Set(t => t.status, "on_hold")
                .Set(t => t.metadata.modifiedDate, DateTime.UtcNow);

            var result = await _tasks.UpdateManyAsync(filter, update);

            Console.WriteLine($"Updated {result.ModifiedCount} tasks to 'on_hold'.");
            Console.WriteLine("\n--- Multiple update operation complete. ---\n");
        }

        // 8. Your choice of any - Aggregation Pipeline for a hierarchical report
        public async Task HierarchicalReport()
        {
            Console.WriteLine("\n--- Generating a hierarchical report using an aggregation pipeline ---\n");

            var pipeline = new BsonDocument[]
            {
                // Match all tasks for the tenant
                new BsonDocument("$match", new BsonDocument("tenantId", TenantId)),

                // Use $lookup to find children
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "tasks" },
                        { "localField", "taskId" },
                        { "foreignField", "parentId" },
                        { "as", "children" }
                    }),
                    
                // Filter to show only root tasks for the report
                new BsonDocument("$match", new BsonDocument("level", 0)),

                // Project the fields for the final report
                new BsonDocument("$project",
                    new BsonDocument
                    {
                        { "_id", 0 },
                        { "taskId", "$taskId" },
                        { "title", "$title" },
                        { "status", "$status" },
                        { "childrenCount", new BsonDocument("$size", "$children") },
                        { "childTitles", new BsonDocument("$map", new BsonDocument
                            {
                                { "input", "$children" },
                                { "as", "child" },
                                { "in", "$$child.title" }
                            })
                        }
                    })
            };

            var reportResults = await _tasks.Aggregate<BsonDocument>(pipeline).ToListAsync();

            Console.WriteLine("Root Tasks and Their Direct Children:");
            foreach (var doc in reportResults)
            {
                Console.WriteLine($"\nRoot Task ID: {doc["taskId"]}");
                Console.WriteLine($"Title: {doc["title"]}");
                Console.WriteLine($"Status: {doc["status"]}");
                Console.WriteLine($"Direct Children Count: {doc["childrenCount"]}");

                var childTitles = doc["childTitles"].AsBsonArray;
                if (childTitles.Count > 0)
                {
                    Console.WriteLine("Children Titles:");
                    foreach (var childTitle in childTitles)
                    {
                        Console.WriteLine($"- {childTitle.AsString}");
                    }
                }
            }
            Console.WriteLine("\n--- Aggregation pipeline operation complete. ---\n");
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // Replace with your actual connection string.
            const string connectionString = "mongodb://localhost:27017";
            const string databaseName = "TaskManagerDB";
            var service = new TaskService(connectionString, databaseName);

            while (true)
            {
                Console.WriteLine("\n---------------------------------------------------");
                Console.WriteLine("Select an operation:");
                Console.WriteLine("1. Insert Hierarchy");
                Console.WriteLine("2. Update Single Task");
                Console.WriteLine("3. Delete Task and its Descendants");
                Console.WriteLine("4. Read Single Task");
                Console.WriteLine("5. Read Multiple Tasks with Date Range");
                Console.WriteLine("6. Complex Filter Query");
                Console.WriteLine("7. Update Multiple Tasks");
                Console.WriteLine("8. Generate Hierarchical Report (Aggregation)");
                Console.WriteLine("9. Exit");
                Console.WriteLine("---------------------------------------------------");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("How many parent records to insert? ");
                        int parents = int.Parse(Console.ReadLine());
                        Console.Write("How many child records per level? ");
                        int children = int.Parse(Console.ReadLine());
                        Console.Write("How many levels deep? ");
                        int levels = int.Parse(Console.ReadLine());
                        await service.InsertHierarchy(parents, children, levels);
                        break;
                    case "2":
                        Console.Write("Enter the taskId to update: ");
                        string updateId = Console.ReadLine();
                        await service.UpdateSingleTask(updateId);
                        break;
                    case "3":
                        Console.Write("Enter the taskId to delete: ");
                        string deleteId = Console.ReadLine();
                        await service.DeleteTask(deleteId);
                        break;
                    case "4":
                        Console.Write("Enter the taskId to read: ");
                        string readId = Console.ReadLine();
                        await service.ReadSingleTask(readId);
                        break;
                    case "5":
                        await service.ReadMultipleTasksByRange();
                        break;
                    case "6":
                        await service.ComplexFilterQuery();
                        break;
                    case "7":
                        await service.UpdateMultipleTasks();
                        break;
                    case "8":
                        await service.HierarchicalReport();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}

