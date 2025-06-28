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

## 🧱 Project Structure
Library/
├── Controllers/                  # API Endpoints
├── Middlewares/                 # Error handling, authentication
├── Extensions/                  # Dependency Injection & Middleware setup
├── Helper/                      # Token + Encryption utilities

Library.Service/
├── BookService/                 
│   └── Dtos/                    # DTOs for Book operations
├── UserService/
│   └── Dto/                     # DTOs for User operations
├── TokenService/                # JWT Token generation
├── TransactionService/
│   └── Dtos/                    # DTOs for Transactions

Library.Repository/              # Data Access Layer

Library.Data/
├── Entites/IdentityEntities/    # Identity Models
├── Migrations/                  # EF Core Migrations

Program.cs                       # Application Entry Point
appsettings.json                 # App Configuration & Secrets

---

## 📦 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/kareemwaleed266/library-management-api.git
cd library-management-api/Library
