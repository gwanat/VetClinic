# VetClinic Management System

## Overview
VetClinic is a modular Veterinary Clinic Management System built with ASP.NET Core MVC, following Clean Architecture principles for clear separation of concerns and scalability. It manages core features like appointments, doctors, patients, and rooms, with full CRUD operations available via both a web interface (MVC and Razor Views) and RESTful APIs. The system includes ASP.NET Core Identity for secure user authentication, multilingual support through resx resource files, and comprehensive unit testing with xUnit. API documentation is provided via Swagger, and the endpoints have been tested using Postman. The solution is structured into five projects: CoreBusiness (entities and rules), UseCases (application logic), Unit_Tests (test coverage), Plugins (extensions), and VetClinic (the main web application). This design ensures maintainability, testability, and flexibility for future improvements.



![image](https://github.com/user-attachments/assets/1f52e699-e554-467a-830b-55c88b9200b5)

## Features
- Appointment Management (CRUD operations)
- Doctor Management
- Patient Management
- Room Allocation and Management
- User Authentication and Authorization with ASP.NET Identity
- Localization Support (Resource files)
- Unit Testing for core business logic
- Extensible plugin architecture
- RESTful API for external integrations

## Technologies
- ASP.NET Core MVC
- Entity Framework Core
- xUnit (Unit Testing)
- C#
- Razor Pages
- ASP.NET Identity
- Localization with .resx files
- Dependency Injection
- GitHub Actions (for CI/CD)

## Project Layers (Clean Architecture)
- CoreBusiness: Contains core entities like Doctor, Patient, Appointment, and Room.
- UseCases: Implements application logic (Add, Update, Delete, Get) for each entity.
- Unit_Tests: Provides test coverage for all use cases.
- Plugins: Placeholder for external integrations or extensions.
- VetClinic (Web): Contains the presentation layer (MVC), APIs, Identity, and database context.

## Tools
- Swagger: Auto-generated API documentation for easy testing.
- Postman: Manual and automated API testing with shared collections.

## Localization
Multilingual support is handled via .resx resource files:

- Resource.resx (default)
- Resource.fr.resx (French)
- Resource.pl.resx (Polish)

##
![image](https://github.com/user-attachments/assets/35ea400d-a470-44e4-ba26-c185f4396c4f)
![image](https://github.com/user-attachments/assets/db2ede0f-a8dd-4fbd-ac12-f21ddf5e316e)
![image](https://github.com/user-attachments/assets/39f68663-355f-47ef-b5f0-1cb9736fabc3)
![image](https://github.com/user-attachments/assets/5d00fdd9-0464-4eb7-9b14-1096ddbfc552)
![image](https://github.com/user-attachments/assets/6cd627c0-3ff9-4e3a-9f53-eca6312b6105)

