# Architectural Decision Records (ADRs) for Virgin Active Club Access POC

## ADR 1: Assumptions Made

### Context

Before begining the development of the Virgin Active Club Access POC, several assumptions were made regarding the operational environment, user behaviors, and technical infrastructure. These assumptions informed the design and technology choices for the project.

### Decision

The key assumptions made include:

1. Users will have access to a device capable of scanning QR codes at entry and exit points.
2. There will be reliable internet connectivity at club locations for cloud synchronization atleast once during the lifecycle of the app.
3. The local database needs to be lightweight and operate independently of network connectivity. Club members are comfortable using QR codes as their primary method for access control.
4. The application needs to be cross-platform to cater to different devices and operating systems.
5. Club members are comfortable using QR codes as their primary method for access control.

### Consequences

These assumptions led to specific design decisions, such as the adoption of .NET MAUI for cross-platform development, the use of SQLite for local data storage, and the implementation of Azure services for cloud-based synchronization. Should any of these assumptions prove incorrect, it may necessitate reevaluation of the chosen technologies or design approaches.

## ADR 2: Choice of .NET MAUI for Cross-Platform Development

### Context

The Virgin Active Club Access POC is intended to be deployed across various platforms, including Windows, macOS, Android, and iOS, to accommodate the diverse devices used at club access points.

### Decision

.NET MAUI was chosen as the development framework due to its ability to support cross-platform application development from a single codebase. This choice allows for the efficient development, maintenance, and deployment of the application across all targeted platforms, while leveraging .NET's robust ecosystem and MAUI's modern UI capabilities.

### Consequences

Choosing .NET MAUI ensures a unified development experience and reduces the time and resources required for developing and testing on multiple platforms. However, this decision also means the project is dependent on the limitations and release cycles of .NET MAUI and its compatibility with future platform updates.

## ADR 3: Utilizing SQLite for Local Data Storage

### Context

The need for reliable, local data storage that operates independently of network connectivity was identified as critical for the access control system, to ensure uninterrupted club member access and data logging at entry and exit points.

### Decision

SQLite was selected for local database storage due to its lightweight nature, self-contained, serverless, and zero-configuration features. It offers the required reliability and independence from network connectivity, ensuring that access logs and member details are always available for the application to function effectively.

### Consequences

Adopting SQLite enables the system to function in offline modes and ensures fast access to data with minimal overhead. However, it also means the system's data handling capabilities are bound by SQLite's performance and storage limitations, which is considered sufficient for the project's scope.

## ADR 4: Creation of a Background Data Synchronization Service

### Context

To maintain data integrity and enable centralized management of access logs and member details, a mechanism for synchronizing data between local databases at club access points and a central cloud server was required.

### Decision

A background service was developed to handle data synchronization tasks. This service runs alongside the main application, periodically syncing data between the local SQLite databases and the Azure SQL cloud database. It is designed to handle intermittent network connectivity and ensure no data loss during transmission.

### Consequences

The implementation of a background synchronization service ensures data consistency across local and cloud storage, supporting centralized data analysis and management. This approach requires careful handling of data conflicts and synchronization errors to prevent data corruption or loss. Additionally, the background service's operation must be optimized to minimize resource consumption and interference with the primary application functionality.
