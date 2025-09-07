---
description: >-
  Amazon DataZone is a fully managed data management service that provides a
  central, searchable catalog for all your data.
icon: tombstone-blank
---

# DataZone

## Amazon DataZone

### 🌟 Overview: Amazon DataZone 🚀

Amazon DataZone is a fully managed data management service that provides a data catalog and governance solution for organizations. It simplifies the process of cataloging, discovering, sharing, and governing data at scale across different business units, teams, and data sources. DataZone acts as a central hub, making it faster and easier for data producers (e.g., data engineers) and data consumers (e.g., business analysts, data scientists) to collaborate on data-driven projects while ensuring that data access is secure and compliant with organizational policies.

<figure><img src="../../../.gitbook/assets/Arch_Amazon-DataZone_64@5x.png" alt="" width="100"><figcaption></figcaption></figure>

The service's core value lies in breaking down data silos and democratizing data access. It enables users to find and understand data assets through a unified, searchable business data catalog, which is enriched with business context and metadata. This approach aligns with the principles of a modern data mesh architecture, where data is treated as a product and ownership is decentralized.

### 🤖 Innovation Spotlight: AI/ML-Powered Data Curation and Data Products

Amazon DataZone is constantly evolving, with a key innovative focus on leveraging artificial intelligence and machine learning. One of the most significant recent innovations is the use of large language models (LLMs) to automatically generate business names and descriptions for data assets. This feature dramatically reduces the manual effort required for data curation, making the catalog more comprehensive and user-friendly from day one.

Furthermore, the introduction of **Data Products** is a game-changer. This feature allows data producers to bundle related data assets (e.g., tables, reports, dashboards) into a single, cohesive unit. For example, a "Marketing Campaign Analysis" data product could contain the marketing campaign data, customer demographics, and sales figures. This simplifies the subscription process for consumers, who can request access to a whole data product with a single approval workflow, rather than subscribing to individual assets one by one. This directly aligns with the "data as a product" philosophy, making data consumption intuitive and business-focused.

***

### ⚡ Problem Statement

A large, multi-national retail corporation, "Global Retail Inc.," is struggling with data silos and a lack of data discoverability. Their data is scattered across different departments (Marketing, Finance, Supply Chain) and various data stores, including Amazon S3 data lakes, Amazon Redshift data warehouses, and on-premises databases.

Data analysts and business intelligence teams frequently spend weeks searching for the right data, understanding its context, and gaining access permissions. The process involves multiple manual requests, emails, and meetings, leading to significant delays in generating reports and insights. The marketing team, for instance, needs customer data from the sales team and product data from the supply chain team to analyze campaign effectiveness, but they face a cumbersome process to find and access the required datasets. This inefficiency hinders their ability to make quick, data-driven decisions and respond to market changes.

### 🤝 Business Use Cases

* **Retail:** A marketing analyst at Global Retail Inc. wants to analyze customer purchasing behavior to optimize a new product launch. Using Amazon DataZone, they can search for "customer purchase history" and "product catalog" in the business data catalog. They discover a curated "Customer Analytics" data product, subscribe to it, and automatically gain access to the necessary data in their designated Amazon Redshift environment.
* **Financial Services:** A data scientist at a bank needs to build a fraud detection model. They require transaction data from the core banking system, credit history data from a third-party service, and customer demographic data. With DataZone, they can find and subscribe to these datasets from different business units, ensuring they have the necessary, well-governed data for their model.
* **Healthcare:** A research team at a pharmaceutical company needs to analyze clinical trial data, patient records, and genomic data. They can use Amazon DataZone to securely discover and access the datasets they need, with automated governance rules and access controls that ensure compliance with regulations like HIPAA.

***

### 🔥 Core Principles

Amazon DataZone operates on a few key principles to achieve its goals:

* **Decentralized Data Ownership (Data Mesh):** It empowers business units to own and manage their data as a product, providing a domain-based approach to data governance. This means the team that produces the data is also responsible for its quality, documentation, and sharing.
* **Self-Service Data Access:** It provides a user-friendly portal where data consumers can discover, understand, and request access to data without needing to go through a complex, manual IT process.
* **Built-in Governance:** It integrates governance directly into the data sharing workflow. Data producers can define policies, and access requests are automatically routed for approval, with fine-grained controls managed through services like AWS Lake Formation.
* **Business-Centric Data Cataloging:** It focuses on business terminology rather than technical jargon. Data assets are enriched with business metadata, glossaries, and descriptions, making them intuitive for non-technical users to find and use.

### 🔥 Core Resources and Terms:

* **Domain:** A high-level organizational construct that serves as the boundary for data sharing and governance. It often aligns with a business unit, like "Marketing" or "Finance."
* **Project:** A collaborative workspace within a domain where teams can work on specific data initiatives. It brings together data assets, users, and analytical environments.
* **Environment:** A logical collection of AWS services (e.g., Amazon Redshift, Amazon Athena, Amazon SageMaker) provisioned for a project. It provides the necessary infrastructure for users to consume and analyze data.
* **Data Source:** A connector to a source of data, such as an AWS Glue Data Catalog, an Amazon Redshift cluster, or an on-premises database. DataZone uses these to automatically ingest metadata.
* **Data Asset:** A single, discoverable data entity in the catalog, such as a table in a database or a file in a data lake.
* **Data Product:** A new feature that allows data producers to bundle related data assets into a single, cohesive unit for specific business use cases.
* **Catalog:** The central repository where all discoverable data assets and data products are stored and enriched with business and technical metadata.
* **Subscription:** A request by a data consumer to get access to a data asset or data product. Access is granted based on predefined workflows and policies.

***

### 📋 Pre-Requirements

To implement a solution with Amazon DataZone, you will need the following services and tools:

* **AWS Account:** The foundational requirement to access all AWS services.
* **AWS IAM Identity Center (formerly AWS SSO):** Recommended for managing user access and authentication to the Amazon DataZone portal and projects.
* **AWS Glue Data Catalog:** The primary data source for automatically cataloging data assets stored in Amazon S3. DataZone uses it to extract technical metadata.
* **AWS Lake Formation:** Essential for providing fine-grained, policy-based access control (row and column-level filtering) to data in S3 and other data stores.
* **Amazon Redshift / Amazon Athena / Amazon S3:** Your data stores where the actual data resides. DataZone integrates with these services to provide governed access.
* **AWS CloudFormation / AWS CDK:** Recommended for automating the creation of DataZone resources and managing the environment as code.
* **Mermaid JS:** Used for creating the data flow diagrams.

***

### 👣 Implementation Steps

Here is a simplified step-by-step guide to setting up a data governance solution with Amazon DataZone for Global Retail Inc.

1. **Set up the Amazon DataZone Domain:**
   * Navigate to the Amazon DataZone console.
   * Create a new "Domain" (e.g., "GlobalRetail"). This will be the central governance hub.
   * Configure the domain with an IAM Identity Center instance for user management.
2. **Define Business Units as Projects:**
   * Create "Projects" within the domain for different business units, such as "Marketing," "Finance," and "SupplyChain."
   * Add the relevant users from each department to their respective projects.
3. **Set up Data Sources:**
   * In the "SupplyChain" project, create a "Data Source" that connects to the AWS Glue Data Catalog where the product and inventory data tables are stored.
   * Run the data source to automatically ingest metadata and create data assets in the project inventory.
4. **Curate and Publish Data Assets:**
   * The data producers in the "SupplyChain" project review the ingested data assets (e.g., `product_catalog_table`).
   * They enrich the assets with business-friendly descriptions, add tags, and associate them with a business glossary.
   * They then "Publish" these curated data assets to the central "GlobalRetail" catalog.
5. **Create a Data Product (Innovation Spotlight):**
   * The data producer in "SupplyChain" can now group the `product_catalog_table` and other relevant assets into a "Data Product" called "Product Data Insights."
   * This data product is also published to the central catalog, simplifying discovery for consumers.
6. **Create an Environment for Analysis:**
   * The "Marketing" project team creates an "Environment" that provisions a new Amazon Redshift cluster. This environment is their workspace.
7. **Discover and Subscribe to Data:**
   * A data analyst in the "Marketing" project logs into the Amazon DataZone portal.
   * They use the search bar to find "Product Data Insights" and see the associated data assets.
   * The analyst submits a "Subscription Request" for the "Product Data Insights" data product.
8. **Approve and Grant Access:**
   * The data owner in the "SupplyChain" project receives the subscription request and reviews it.
   * Upon approval, DataZone automatically provisions the necessary AWS Lake Formation permissions, granting the "Marketing" project read-only access to the data in the Redshift cluster within their environment.
9. **Analyze Data:**
   * The marketing analyst can now connect their BI tool (e.g., Tableau, Amazon QuickSight) to their Redshift environment.
   * They can securely and immediately query the product data without manual permission granting or data movement.

***

### 🗺️ Data Flow Diagram

**Diagram 1: Data Curation and Publishing**

This diagram illustrates how data is ingested from source systems, curated, and then published to the central Amazon DataZone catalog.

```mermaid
graph TD
    subgraph Data Producer (SupplyChain)
        A[Data Source: AWS Glue Data Catalog]
        B[Ingest Metadata]
        C[Project Inventory]
        D[Curate and Enrich Assets]
        E[Publish to Catalog]
    end

    subgraph Data Consumer (Marketing)
        F[Amazon DataZone Portal]
        G[Search & Discover]
        H[Subscribe to Assets]
    end

    I[Central DataZone Catalog]
    J[Data Store: Amazon S3 & Amazon Redshift]

    J --> A
    A --> B
    B --> C
    C --> D
    D --> E
    E --> I

    F --> G
    G --> I
    I --> H
    H --> F
    F --> D
```

**Diagram 2: Data Subscription and Consumption**

This diagram shows the process of a data consumer requesting access to data and how DataZone orchestrates the access grant using AWS Lake Formation.

```mermaid
graph TD
    subgraph Data Producer (SupplyChain)
        A[Published Data Asset]
        B[Data Owner]
    end

    subgraph Amazon DataZone
        C[Central Catalog]
        D[Subscription Request]
        E[Access Grant Workflow]
    end

    subgraph Data Consumer (Marketing)
        F[Data Analyst]
        G[DataZone Portal]
        H[Analysis Environment: Amazon Redshift]
        I[BI Tool: Tableau/QuickSight]
    end

    subgraph AWS Services
        J[AWS Lake Formation]
        K[Data Store: Amazon S3]
    end

    F --> G
    G --> C
    G --> D
    D --> B[Data Owner]
    B --> E[Approve/Deny Request]
    E --> J[Update Permissions]
    J --> H[Access Granted]
    H --> I[Query Data]
    K --> J
    J --> H
```

**Diagram 3: Data Product Creation and Consumption**

This diagram highlights the new "Data Product" innovation, where multiple assets are bundled for streamlined access.

```mermaid
graph TD
    subgraph Data Producer (Finance)
        A[Data Asset 1: Sales Data]
        B[Data Asset 2: Cost Data]
        C[Data Asset 3: Inventory Data]
        D[Create "Financial Reports" Data Product]
        E[Publish Data Product to Catalog]
    end

    subgraph Amazon DataZone
        F[Central Catalog]
        G[Subscription Request for Data Product]
        H[Single Approval Workflow]
    end

    subgraph Data Consumer (CEO)
        I[Business User]
        J[DataZone Portal]
        K[Analytics Environment: Amazon QuickSight]
    end

    A --> D
    B --> D
    C --> D
    D --> E
    E --> F

    I --> J
    J --> F
    J --> G
    G --> H
    H --> K[Access Granted to all Assets]
    K --> I
```

***

### 🔒 Security Measures

* **Least Privilege IAM Roles:** Use fine-grained IAM roles for DataZone service roles and for users. Grant only the necessary permissions required for a specific task. For example, a data consumer's role should have read-only access to specific data, while a data producer's role can have write and manage permissions.
* **Integration with AWS IAM Identity Center:** Centralize user and group management with IAM Identity Center to simplify access control and ensure consistency across your organization.
* **Fine-Grained Access with AWS Lake Formation:** Leverage Lake Formation's tag-based access controls to grant row and column-level permissions. This ensures that even after a subscription is approved, users can only see the data they are authorized to view (e.g., a marketing analyst can only see data for their region).
* **Encryption:** Ensure all data at rest (e.g., in S3, Redshift) and in transit is encrypted using AWS KMS.
* **VPC Endpoints:** Use Amazon VPC Endpoints to connect to DataZone from within your VPC, keeping traffic within the AWS network and improving security.
* **AWS CloudTrail:** Enable CloudTrail to log all API calls made to Amazon DataZone, providing an audit trail for all data access and governance activities.

***

### ⚖️ When to use and when not to use

✅ **When to use Amazon DataZone:**

* Your organization has a large number of data silos and struggles with data discoverability.
* You have diverse data teams (data scientists, analysts, business users) who need to access and collaborate on data.
* You are looking to implement a data mesh architecture and need a tool to manage decentralized data ownership and governance.
* You need a self-service data portal where users can easily find, understand, and request access to data.
* You need to enforce a consistent, policy-based governance framework across various data sources (S3, Redshift, etc.).

❌ **When not to use Amazon DataZone:**

* You have a very small team and a handful of data sources where a simple permissions model (e.g., direct IAM policies) is sufficient.
* Your data is highly centralized and managed by a single team, and there is no need for a self-service model or cross-departmental data sharing.
* Your primary need is data transformation and ETL/ELT pipelines, which are better served by services like AWS Glue or Amazon EMR. DataZone is a governance layer on top of these, not a replacement.
* You do not want to adopt a data mesh or decentralized governance model.

***

### 💰 Costing Calculation

Amazon DataZone's pricing is based on a pay-as-you-go model with no upfront fees. The cost is calculated based on four key dimensions:

1. **Requests:** The number of API requests made to the DataZone service. A certain number of requests are free per month.
2. **Metadata Storage:** The amount of metadata (e.g., business glossaries, descriptions, tags) stored in the catalog.
3. **Compute:** The compute time used for tasks like ingesting metadata from data sources or automatically generating business descriptions. This is measured in "compute units."
4. **Recommendations:** The number of input and output tokens consumed by the AI/ML-powered recommendations for metadata generation.

**Sample Calculation:**

Let's assume a medium-sized enterprise with the following monthly usage:

* **Requests:** 200,000 API requests.
* **Metadata Storage:** 1 GB of metadata stored.
* **Compute:** 2 compute units for data source runs.
* **Recommendations:** 500,000 input tokens and 100,000 output tokens.

_(Note: Pricing is for illustration and may vary. Please check the official AWS pricing page for current rates.)_

* **Requests:** (200,000 - 4,000 free requests) / 100,000 \* $10 = $19.60
* **Metadata Storage:** (1024 MB - 20 MB free) \* ($0.4/1024 MB) = $0.39
* **Compute:** (2 - 0.2 free units) \* $1.776 = $3.20
* **Recommendations:** (500,000 / 1000) \* $0.015 + (100,000 / 1000) \* $0.075 = $7.50 + $7.50 = $15.00
* **Total Estimated Cost:** $19.60 + $0.39 + $3.20 + $15.00 = **$38.19 / month**

**Efficient Way of Handling Cost:**

* **Utilize the Free Tier:** Be aware of the monthly free usage limits for requests, metadata storage, and compute.
* **Optimize Data Source Runs:** Schedule data source runs for metadata ingestion to run at appropriate intervals (e.g., once a day or week) rather than continuously to minimize compute costs.
* **Curate Smartly:** Focus on curating and enriching the most critical datasets first, as metadata storage costs are based on volume.

***

### 🧩 Alternative Services in AWS/Azure/GCP/On-Premise

| Service                                  | Platform      | Key Comparison/Difference                                                                                                                                                                                                  |
| ---------------------------------------- | ------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Amazon DataZone**                      | AWS           | Fully managed, business-centric data cataloging and governance platform with deep integration into the AWS ecosystem. Focuses on data mesh and self-service.                                                               |
| **Azure Purview (now Microsoft Fabric)** | Azure         | A unified data governance solution that helps you manage and govern your on-premises, multi-cloud, and SaaS data. Similar to DataZone, it provides data discovery and cataloging with a focus on data lineage.             |
| **Dataplex**                             | GCP           | An intelligent data fabric that unifies distributed data and automates governance for analytics and machine learning. It provides a data catalog and governance capabilities across different data stores in Google Cloud. |
| **Informatica Enterprise Data Catalog**  | On-Prem/Cloud | A comprehensive data cataloging solution that provides AI-powered discovery, end-to-end data lineage, and collaborative features. Requires more manual setup and management compared to cloud-native services.             |
| **Collibra Data Governance Center**      | On-Prem/Cloud | A leading data governance platform that offers a wide range of capabilities beyond just a data catalog, including data quality, lineage, and stewardship. Can be more complex and costly to implement.                     |

**On-Premise Alternative Data Flow (Manual Process)**

This diagram shows a typical on-premise, manual process for data discovery and access, highlighting the complexity that DataZone aims to solve.

```mermaid
graph TD
    subgraph Data Producer (IT)
        A[Data Store: On-Prem Database]
        B[Manual Metadata Extraction]
        C[Create Spreadsheet of Metadata]
        D[Store Spreadsheet on SharePoint/Internal Wiki]
    end

    subgraph Data Consumer (Analyst)
        E[Need Data for Report]
        F[Search Internal Wiki/Ask Colleagues]
        G[Find Spreadsheet with Table Info]
        H[Send Email Request for Access]
        I[Wait for IT to Manually Grant Permissions]
        J[Manual Connection and Query]
    end

    A --> B
    B --> C
    C --> D
    D --> F
    F --> G
    G --> H
    H --> I
    I --> J
```

***

### ✅ Benefits

* **Accelerated Time-to-Insight:** Reduces the time data consumers spend searching for and gaining access to data from weeks to minutes.
* **Enhanced Governance and Security:** Ensures that data access is consistently governed and compliant with organizational policies, reducing security risks.
* **Improved Collaboration:** Provides a central platform for data producers and consumers to collaborate on data projects, breaking down organizational silos.
* **Reduced Operational Overhead:** Automates manual and error-prone processes of metadata collection, access requests, and permission grants.
* **Democratized Data Access:** Empowers business users and non-technical roles to be more self-sufficient in their data exploration and analysis.
* **Cost-Effective:** Pay-as-you-go pricing model with no upfront investment in building and maintaining a custom data catalog solution.

***

### 📝 Summary

Amazon DataZone is a pivotal service for organizations striving to become more data-driven. It is an enterprise-wide data catalog and governance service that simplifies data discovery, sharing, and access. By integrating with key AWS analytics services and providing a self-service portal, DataZone enables a modern, decentralized data mesh architecture where data is treated as a product. The recent innovations in AI/ML-powered metadata generation and data product bundling make it an even more powerful and efficient solution for managing data at scale.

**Top 5 Things to Keep in Mind:**

1. **It's a Governance Layer, Not an ETL Tool:** DataZone doesn't move or transform your data; it governs access to it. It sits on top of your existing data stores and analytics services.
2. **Focus on Business Context:** Its true value lies in enriching data with business metadata, not just technical details. A well-defined business glossary is crucial for adoption.
3. **Start with a Single Domain:** Begin with a pilot project in one domain or business unit to define your governance model before rolling it out across the entire organization.
4. **IAM Identity Center is Key:** For an optimal user experience and seamless access control, use IAM Identity Center to manage your users and groups.
5. **Leverage the `Data Product` Feature:** This new feature is a major efficiency driver. Encourage your data producers to bundle related assets to simplify data consumption for their internal customers.

**In short, what Amazon DataZone is all about:** Amazon DataZone is a fully managed data management service that provides a central, searchable catalog for all your data. It enables data producers to easily share and govern access to their data, and it allows data consumers to quickly discover, understand, and subscribe to the data they need for their projects, all through a secure, self-service portal.

### 🔗 Related Topics

* **AWS Glue:** The primary service for ETL, data cataloging, and metadata management, which DataZone deeply integrates with.
* **AWS Lake Formation:** The core service for fine-grained access control on data lakes. DataZone uses Lake Formation policies to enforce governance.
* **Data Mesh Architecture:** A conceptual approach to data management that Amazon DataZone is built to enable.
* **Amazon Redshift & Amazon Athena:** The data stores where data governed by DataZone typically resides.
* **Amazon QuickSight:** The business intelligence service that data consumers can use to analyze data they've accessed via DataZone.
