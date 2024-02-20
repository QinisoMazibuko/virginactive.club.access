# Virgin Active Club Access POC

## Overview

The Virgin Active Club Access System is a comprehensive POC designed to streamline the process of managing club access for members. Leveraging QR code technology, the system enables members to easily enter and exit club premises. The application ensures accurate tracking of member movements by synchronizing entry and exit logs between local installations and a centralized cloud server.

https://github.com/QinisoMazibuko/virginactive.club.access/blob/main/src/virginactive.club.access/Resources/Images/CheckIn.png
https://github.com/QinisoMazibuko/virginactive.club.access/blob/main/src/virginactive.club.access/Resources/Images/CheckOut.png
## Features

- **QR Code Scanning:** Members can scan their unique QR codes at designated entry and exit points using a webcam, facilitating hassle-free access control.
- **Local Database Storage:** The system employs SQLite for the robust storage of member details and access logs locally, ensuring data availability even in offline scenarios.
- **Cloud Synchronization:** Access data is seamlessly synced between the local databases and a central Azure SQL database, enabling centralized access management and monitoring.
- **Cross-Platform Compatibility:** Built with .NET 7 and .NET MAUI, the application supports deployment across multiple operating systems, making it versatile for various access point installations.

## Technologies and Packages Used

- **.NET 7:** For backend development, ensuring modern, efficient, and scalable application logic.
- **.NET MAUI:** Facilitates the development of native cross-platform applications with a single, shared codebase.
- **SQLite:** Utilized for local database storage, offering lightweight, self-contained SQL database engine.
- **Entity Framework Core:** ORM used for database operations, simplifying data access and manipulation.
- **Azure SQL Database:** Cloud database service for storing synchronized data, providing high availability, security, and scalability.

## Application Structure

The application adopts a layered architecture to separate concerns, enhance maintainability, and facilitate scalability:

- **Presentation Layer (MAUI App):** Contains the UI components and logic for QR code scanning and displaying user feedback.
- **Business Logic Layer (Services):** Implements the core functionality including access control logic, data synchronization, and interactions with the database.
- **Data Access Layer (Repository):** Manages data operations, abstracting the access to local SQLite and Azure SQL databases.
- **Core Layer (Models):** Defines the data models and interfaces used throughout the application.

## Getting Started

### Prerequisites

- .NET 6 or .NET 7 SDK installed on your machine.
- Visual Studio Code or another IDE compatible with .NET and MAUI development.
- An active Azure account for configuring cloud services.

### Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/your-repository/virginactive-club-access.git
   ```

2. **Install Dependencies:**

   ```bash
   dotnet restore
   ```

3. **Set Up Local Database:**
   -Apply EF Core migrations to set up your SQLite database:

   ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
   ```

4. **Configure Azure Services:**
   -Follow the instructions in the Azure documentation to set up Azure SQL Database and Azure Functions for cloud synchronization.

### Running the Application

    ```bash
      dotnet run
    ```
