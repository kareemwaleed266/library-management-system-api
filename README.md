# 📚 Library Management System API

A secure and scalable **ASP.NET Core Web API** for managing library data including books, users, roles, and transactions.  
Built using **Entity Framework Core**, **ASP.NET Identity**, **JWT Authentication**, and follows **Clean Architecture** principles.

---

## 🚀 Features

- ✅ **User Authentication & Authorization** with JWT and ASP.NET Identity
- 📘 **Book & Library Management** (CRUD operations)
- 🧩 **Role-based Access Control**
- 🔐 **Token security** with encryption and claims
- 🔍 **Filtering, Searching, Sorting, Pagination**
- 🧾 **Swagger UI** for testing endpoints
- 📊 **Centralized Logging** via NLog
- 🔁 **Layered Clean Architecture** (Service/Repo/API Separation)
- 📦 **DTO Mapping** using AutoMapper

---

## 🛠️ Tech Stack

| Layer        | Technology                      |
|--------------|---------------------------------|
| Framework    | ASP.NET Core 6.0                |
| ORM          | Entity Framework Core           |
| Database     | SQL Server                      |
| Auth         | ASP.NET Identity + JWT          |
| Docs         | Swagger (Swashbuckle)           |
| Logging      | NLog                            |
| Mapping      | AutoMapper                      |

---
## 🧱 Project Structure Overview

### 🔹 Main API Layer (Presentation Layer)
- **Controllers**  
  Contains all RESTful API endpoints (e.g., BookController, AccountController).
  
- **Middlewares**  
  Handles cross-cutting concerns like error handling, request logging, and JWT validation.

- **Extensions**  
  Used to organize service and middleware registrations cleanly into extension methods.

- **Helper**  
  Contains utility classes such as token generation and encryption methods.

---

### 🔹 Service Layer (Business Logic)
Located in the `Library.Service` project:
- **BookService**  
  Handles all operations related to books.
  
- **UserService**  
  Manages users and roles.

- **TransactionService**  
  Handles borrowing and returning operations.

- **TokenService**  
  Responsible for generating and refreshing JWT tokens.

- **Dtos**  
  Each service has a nested `Dtos` folder containing Data Transfer Objects to ensure separation from domain models.

---

### 🔹 Data Access Layer
- **Library.Repository**  
  Houses repository classes that abstract the database logic using EF Core.

- **Library.Data**  
  - `Entities/IdentityEntities`: Contains ASP.NET Identity models (Users, Roles, Claims).
  - `Migrations`: Entity Framework migration files for database versioning.

---

### 🔹 Root Configuration
- **Program.cs**  
  The entry point of the application. Configures services, middleware, and app pipeline.

- **appsettings.json**  
  Holds configuration for database connections, JWT tokens, and encryption keys.

---

## 📦 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/kareemwaleed266/library-management-api.git
cd library-management-api/Library
