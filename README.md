# SpendWise
*SpendWise* is a robust expense tracker application that provides a well-documented and flexible API for interacting with its features.

## About

*SpendWise* is a comprehensive, user-friendly expense tracker designed as a Modular Monolith. This robust application was built using the innovative design technique - **Event Storming**, and it's core is based on the well-regarded framework - [Inflow](https://github.com/devmentors/Inflow). The architecture of the project have been meticulously mapped out in a comprehensive manner on the [Miro whiteboard](https://miro.com/app/board/uXjVMZ6udaI=/?share_link_id=178278087749), making it readily accessible for both team collaboration and user reference.

Written in [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0), it leverages the platform's advanced features and capabilities. The use of .NET 8.0 ensures compatibility with modern interfaces, secure networking, high performance, and an overall optimized experience.

Thanks to its Monolithic architecture, *SpendWise* benefits from shared-memory access, consistency, simplicity, and maintains the resilience-guarding complexity in check. The modular structure ensures each module can be developed, updated, replaced, and even scaled independently, adding further to its adaptability and extendibility. 

*SpendWise* is not just a software application, but a tool of empowerment, designed to help users take control of their finances like never before.

## Prerequisites
Make sure you have installed:
* [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [PostgreSQL](https://www.postgresql.org/download/)

## Getting Started
Clone the repository
<br/>
SSH:

```
git clone git@github.com:Kurdzielson/SpendWise.git 
```
HTTPS:
```
git clone https://github.com/Kurdzielson/SpendWise.git
```
## Configuration

Before running the application, you may need to modify the application settings file `appsettings.json`.

This file should be located in the root directory of the project.

Please update the relevant keys and values to match your environment.

**IMPORTANT**: Do not commit this file with your actual credentials and make sure not to publish any sensitive data.

_Example of `appsettings.json` configuration_

## Starting the application


Start API located under Bootstrapper project:

```
cd src/Bootstrapper/Inflow.Bootstrapper
dotnet run
```
## Modules

The Autonomous Modular Monolithic architecture of SpendWise ensures independence of the various modules. Each module is self-contained, allowing for segregated development, testing, and scaling, improving maintainability and efficiency. Despite their independence, cohesive functionality is achieved through well-defined APIs or Integration Events connecting the modules. This architecture bolsters both productivity and code quality.

### Users Module
The Users Module is responsible for managing the user identities and associated operations. This includes registration, login, and managing permissions for users. It ensures secure authentication and authorization for the application, providing robust access control.

### Customers Module
The Customers Module handles all operations related to customers. It offers services like creating customer profiles, completing customer profiles, verifying customer identities, and browsing through customer data. It effectively manages all the necessary customer-related functionalities.

### Expenses Module
The Expenses Module manages all the expense-related data and operations. This includes recording new expenses, categorizing expenses, tracking expenses over time, and generating detailed expense reports. It ensures that users can effectively track, manage, and analyze their expenses.

## In Progress

The following items are currently in development:

* Incomes Module
* Currencies Module with Foreign Exchange JOG
* Notifications Module (e-mails)

Please note that while these features are in progress, they are not yet available in the main version of the app. Stay tuned for updates!
