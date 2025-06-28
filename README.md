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
├── Controllers/ → API Endpoints
├── Middlewares/ → Error handling, auth
├── Extensions/ → DI & Middleware Config
├── Helper/ → Token + Encryption Helpers

Library.Service/
├── BookService/ → Book logic
│ └── Dtos/
├── UserService/ → User & Role logic
│ └── Dto/
├── TokenService/
├── TransactionService/
│ └── Dtos/

Library.Repository/ → Data Access

Library.Data/
├── Entites/IdentityEntities/
├── Migrations/

Program.cs → Entry Point
appsettings.json → Config & Secrets

yaml
Copy
Edit

---

## 📦 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/kareemwaleed266/library-management-api.git
cd library-management-api/Library
