---
description: >-
  AWS Directory Services provides multiple options for directory services in the
  cloud, enabling you to manage users, groups, and devices.
icon: pallet-boxes
---

# Resources

***

## Simple AD

**Simple AD** is a Microsoft Active Directory-compatible directory on AWS. It is designed for small to medium-sized businesses and provides a cost-effective way to manage users and groups.

<figure><img src="../../../../.gitbook/assets/image (1).png" alt=""><figcaption></figcaption></figure>

### **Key Features**

* **Compatibility:** Simple AD is compatible with Microsoft Active Directory, allowing you to use standard AD tools and protocols.
* **Ease of Use:** It is easy to set up and manage, with a simplified administration interface.
* **Scalability:** It can scale to support up to 5,000 users and 500 groups.
* **Integration:** It integrates with other AWS services, such as Amazon WorkSpaces, Amazon RDS, and Amazon EC2.
* **Cost-Effective:** It is more cost-effective compared to AWS Managed Microsoft AD, making it suitable for smaller organizations.

### **Use Cases**

* Small to medium-sized businesses needing a directory service.
* Organizations looking for a cost-effective solution.
* Environments where simplicity and ease of use are prioritized.

***

## AD Connector

**AD Connector** is a directory gateway that allows you to connect your on-premises Microsoft Active Directory to AWS. It enables you to extend your existing directory to AWS without the need to replicate or synchronize your entire directory.

<figure><img src="../../../../.gitbook/assets/image (2).png" alt=""><figcaption></figcaption></figure>

### **Key Features**

* **Directory Gateway:** Acts as a gateway to connect your on-premises AD to AWS services.
* **No Replication:** Does not replicate or synchronize your entire directory, reducing complexity and cost.
* **Integration:** Integrates with AWS services like Amazon WorkSpaces, Amazon RDS, and Amazon EC2.
* **Security:** Provides secure access to AWS resources using your existing AD credentials.

### **Use Cases**

* Organizations with an existing on-premises AD that want to extend it to AWS.
* Environments where directory replication is not desired.
* Scenarios where secure access to AWS resources using existing AD credentials is required.

***

## AWS Managed Microsoft AD

**AWS Managed Microsoft AD** is a fully managed Microsoft Active Directory service that is compatible with Microsoft Active Directory. It provides a highly available and scalable directory service.

<figure><img src="../../../../.gitbook/assets/image (3).png" alt=""><figcaption></figcaption></figure>

### **Key Features**

* **Fully Managed:** AWS handles the setup, maintenance, and management of the directory.
* **High Availability:** Provides multi-AZ deployment for high availability and fault tolerance.
* **Scalability:** Can scale to support up to 50,000 users and 50,000 groups.
* **Compatibility:** Fully compatible with Microsoft Active Directory, allowing you to use standard AD tools and protocols.
* **Integration:** Integrates with a wide range of AWS services, including Amazon WorkSpaces, Amazon RDS, and Amazon EC2.
* **Security:** Supports advanced security features like fine-grained password policies, group policies, and trust relationships.

### **Use Cases**

* Large enterprises needing a highly available and scalable directory service.
* Organizations requiring advanced AD features and integration with AWS services.
* Environments where a fully managed directory service is preferred.

***

## Summary

* **Simple AD:** Best for small to medium-sized businesses needing a cost-effective and easy-to-use directory service.
* **AD Connector:** Ideal for organizations with an existing on-premises AD that want to extend it to AWS without replication.
* **AWS Managed Microsoft AD:** Suitable for large enterprises needing a highly available, scalable, and fully managed directory service with advanced AD features.

Each of these directory services caters to different needs and use cases, allowing organizations to choose the one that best fits their requirements.
