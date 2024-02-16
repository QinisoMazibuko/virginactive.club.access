# Virgin Active Club Access System

## Overview

The Virgin Active Club Access System is designed to facilitate seamless access control for club members using QR codes. The system allows members to enter and exit club premises by scanning their unique QR codes, with entry and exit logs synchronized between local machines and a central cloud server. This ensures efficient, near real-time management and monitoring of access across club locations.

## Features

- **QR Code Scanning:** Enables members to scan QR codes at entry and exit points using a webcam.
- **Local Database Storage:** Utilizes SQLite for storing member details and access logs locally.
- **Cloud Synchronization:** Syncs access data between local databases and a central Azure SQL database for centralized management.
- **Cross-Platform Compatibility:** Developed with .NET 6/7 and .NET MAUI for compatibility across different operating systems, ensuring the system can be installed at various access points.

## Getting Started

### Prerequisites

- .NET 6 or .NET 7 SDK
- Visual Studio Code or another compatible IDE
- Azure account for setting up cloud services

### Installation

1. **Clone the Repository:**

2. **Navigate to the Project Directory:**

3. **Install Dependencies:**

- Restore the .NET project dependencies:

  ```
  dotnet restore
  ```

4. **Set Up Local Database:**

- Apply EF Core migrations to set up your SQLite database:

  ```
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

5. **Configure Azure Services:**

- Follow the [Azure documentation](https://docs.microsoft.com/en-us/azure/) to set up Azure SQL Database and Azure Functions for cloud synchronization.

### Running the Application

- Launch the application locally:
