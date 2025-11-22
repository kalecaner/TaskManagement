

> **A modular, layered ASP.NET Core MVC application demonstrating real-world patterns, loose coupling, and enterprise workflows.**

---

## 📖 Overview

**TaskManagement** is a professional showcase of modern web development using **.NET 7** and **ASP.NET Core MVC**. It is designed to bridge the gap between theory and practice by implementing a robust **Clean Architecture (Onion Architecture)**.

This repository serves as a comprehensive guide for developers looking to master:
* **CQRS** (Command Query Responsibility Segregation) via **MediatR**.
* **Repository & Unit of Work** patterns.
* **Role-Based Access Control (RBAC)** with separate Admin/Member areas.
* **Validation Pipelines** using FluentValidation.

Whether you are learning enterprise patterns or looking for a solid boilerplate for your next MVC project, TaskManagement provides a complete, end-to-end stack.

---

## 🚀 Key Features

### 🏗️ Architecture & Design
* **Clean Architecture:** Strict separation of concerns (Domain, Application, Infrastructure, UI).
* **CQRS Pattern:** Decoupled request handling using **MediatR** pipeline behaviors.
* **Data Access Abstraction:** Generic and specific Repositories with Unit of Work.
* **Validation:** Server-side validation logic decoupled using **FluentValidation**.

### 🛠️ Functional Capabilities
* **Multi-Area UI:** Distinct razor views for **Admin** and **Member** portals.
* **Advanced Task Management:** Create, assign, prioritize, and track tasks.
* **Notification System:** Real-time-like notifications with read/unread tracking.
* **Reporting:** Visual components for task analytics and reporting.
* **User Management:** Complete profile management, role assignment, and secure authentication.
* **File Handling:** Support for file attachments within tasks.
* **Security:** Cookie-based authentication, Authorization policies, and Password Reset flows (SMTP).

---

## ⚡ Technology Stack

| Category | Technologies |
| :--- | :--- |
| **Core Framework** | .NET 7, C# 11, ASP.NET Core MVC |
| **Data Access** | Entity Framework Core (EF Core), SQL Server |
| **Application Logic** | MediatR, AutoMapper (DTOs), FluentValidation |
| **UI & Frontend** | Razor Pages/Views, Bootstrap 5, AJAX, jQuery |
| **Tools & DevOps** | Docker support (ready), Git |

---

## 📂 Architecture Overview

The solution follows a strict dependency rule where inner layers have no knowledge of outer layers:

1.  **`TaskManagement.Domain`**: The core. Contains Entities (`AppUser`, `AppTask`, etc.) and Enums. No external dependencies.
2.  **`TaskManagement.Application`**: Business logic. Contains DTOs, Interfaces, MediatR Handlers, and Validators.
3.  **`TaskManagement.Persistance`**: Infrastructure. Implements EF Core `DbContext`, Repositories, and Database Migrations.
4.  **`TaskManagement.UI`**: The entry point. Contains Controllers, Views (Areas), and `Program.cs` configuration.

---

## 🛠️ Local Setup & Installation

Follow these steps to get the application running on your local machine.

### Prerequisites
* [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
* SQL Server (LocalDB or Full Instance)

### 1. Clone the Repository
```bash
git clone [https://github.com/kalecaner/TaskManagement.git](https://github.com/kalecaner/TaskManagement.git)
cd TaskManagement