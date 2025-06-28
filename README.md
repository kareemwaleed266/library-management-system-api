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

### **Presentation Layer** – (API)
- `Controllers/`: RESTful endpoints for HTTP communication  
- `Middlewares/`: Global error handling, JWT validation  
- `Extensions/`: Service and middleware setup helpers  
- `Helper/`: Token creation and encryption utilities

---

### **Business Layer** – (Services)
Located in `Library.Service/`:
- `BookService/`: Business logic for books  
- `UserService/`: Manages users and roles  
- `TransactionService/`: Borrow and return operations  
- `TokenService/`: Handles JWT token creation and refreshing  
- `Dtos/`: Each service has its own DTOs to decouple data models

---

### **Data Access Layer** – (Repositories)
- `Library.Repository/`: Contains repositories that interact with the database using EF Core

---

### **Infrastructure Layer** – (Database & Identity)
- `Library.Data/`:  
  - `Entities/IdentityEntities/`: Identity user/role entities  
  - `Migrations/`: EF Core migration files

---

### ⚙️ App Entry & Config
- `Program.cs`: Main app entry point (DI setup, Middleware, App config)  
- `appsettings.json`: DB connection strings, JWT keys, and app secrets

---

## 📦 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/kareemwaleed266/library-management-api.git
cd library-management-api/Library
