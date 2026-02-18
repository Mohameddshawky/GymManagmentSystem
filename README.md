<div align="center">

# 🏋️ Gym Management System

### *A full-featured, role-based web application to streamline gym operations — from member onboarding to session scheduling.*



![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core_MVC-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-15.1-BE4B48?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)
![Repo Size](https://img.shields.io/github/repo-size/Mohameddshawky/GymManagmentSystem?style=for-the-badge)

</div>

---

## 📖 About The Project

Managing a gym involves juggling dozens of moving parts: tracking member health records, scheduling training sessions, assigning trainers, managing subscription plans, and monitoring membership statuses — all at once.

**Gym Management System** is a production-ready, multi-layer web application built with **ASP.NET Core MVC** and **Entity Framework Core** that centralises all of these operations into a single, clean administrative interface.

> **Why this project?**
> Most gym management solutions are either overly complex enterprise tools or simple spreadsheets. This project fills the gap by providing a structured, maintainable, and extensible codebase that demonstrates real-world software engineering practices — including a strict 3-layer architecture, the Repository & Unit of Work patterns, AutoMapper-based DTO mapping, and ASP.NET Core Identity for secure authentication.

---

## 🛠️ Built With

| Layer | Technology |
|---|---|
| **Presentation** | ASP.NET Core MVC 8, Razor Views, Bootstrap |
| **Business Logic** | C# Service Layer, AutoMapper 15.1 |
| **Data Access** | Entity Framework Core 8, SQL Server |
| **Authentication** | ASP.NET Core Identity |
| **ORM Mapping** | AutoMapper (Profiles per entity) |
| **DI Container** | Built-in .NET DI (Scoped services) |
| **Runtime** | .NET 8 |

---

## ✨ Key Features

- 👤 **Member Management** — Full CRUD for gym members, including profile photos, personal details, and address information.
- 🏥 **Health Records** — Track each member's height, weight, blood type, and medical notes as an owned entity.
- 📅 **Session Scheduling** — Create and manage training sessions with capacity limits, date ranges, categories, and assigned trainers.
- 💳 **Membership & Plans** — Define subscription plans (name, price, duration), assign them to members, and automatically compute Active/Expired status.
- 🧑‍🏫 **Trainer Management** — Manage trainers with specialties and link them to sessions.
- 🔐 **Secure Authentication** — Role-based access control via ASP.NET Core Identity with seeded admin roles and users on startup.
- 📊 **Analytics Service** — Dedicated analytics service layer for gym-wide reporting and statistics.
- 📎 **Attachment Service** — Centralised file/image upload handling for member photos and other assets.

---

## 🏗️ Architecture & Design Patterns

The solution is structured around a strict **3-Layer Architecture**, enforcing a clean separation of concerns and making each layer independently testable and maintainable.

```
GymManagmentSystemSolution/
├── GymManagmentDAL/          ← Data Access Layer
│   ├── Entites/              ← Domain Entities & Enums
│   ├── Data/
│   │   ├── Contexts/         ← EF Core DbContext
│   │   └── DataSeed/         ← Startup data seeding
│   └── Repositories/         ← Generic + Specific Repositories, Unit of Work
│
├── GymManagmentBLL/          ← Business Logic Layer
│   ├── Services/             ← Service interfaces & implementations
│   ├── ViewModels/           ← DTOs / ViewModels per feature
│   └── Mapping/              ← AutoMapper profiles
│
└── GymManagmentPL/           ← Presentation Layer
    ├── Controllers/          ← MVC Controllers
    ├── Views/                ← Razor Views
    └── wwwroot/              ← Static assets
```

### Design Patterns Applied

| Pattern | Implementation |
|---|---|
| **Repository Pattern** | `GenericRepository<T>` + specialised repositories (`SessionRepository`, `MemberShipRepository`, `MemberSessionRepository`) |
| **Unit of Work** | `UnitOfWork` coordinates all repositories under a single `DbContext` transaction |
| **Service Layer** | `IMemberService`, `ITrainerService`, `ISessionService`, `IPlanService`, `IMemberShipService`, `IAnalyticsService`, `IAttachmentService`, `IAccountService` |
| **DTO / ViewModel Mapping** | AutoMapper profiles per entity (`MemberProfile`, `TrainerProfile`, `SessionProfile`, `PlanProfile`, `HealthRecordProfile`, `MemberShipProfile`, `MemberSessionProfile`) |
| **Dependency Injection** | All services and repositories registered as `Scoped` in `Program.cs` |
| **Owned Entity** | `HealthRecord` is an EF Core owned entity embedded within `Member` |

---

## 🗄️ Database Schema

The core domain model revolves around **Members** and **Sessions**, connected through a many-to-many join via `MemberSession`.

```
ApplicationUser (ASP.NET Identity)
│
GymUser (abstract base)
├── Member
│   ├── HealthRecord (Owned Entity — Height, Weight, BloodType, Note)
│   ├── MemberShips  ──────────────────► MemberShip ──► Plan
│   └── MemberSessions ────────────────► MemberSession ──► Session
│
└── Trainer
    └── TrainerSessions ────────────────► Session
                                              └── Category
```

### Core Entities

| Entity | Key Properties | Relationships |
|---|---|---|
| `Member` | Photo, inherits GymUser | Has one `HealthRecord`, many `MemberShips`, many `MemberSessions` |
| `Trainer` | Specialties (enum) | Has many `Sessions` |
| `Session` | Description, StartDate, EndDate, Capacity | Belongs to `Category` & `Trainer`; has many `MemberSessions` |
| `MemberShip` | EndDate, computed `Status` (Active/Expired) | Belongs to `Member` & `Plan` |
| `Plan` | Name, Description, Price, DurationDays, IsActive | Has many `MemberShips` |
| `HealthRecord` | Height, Weight, BloodType, Note | Owned by `Member` |
| `Category` | Name | Groups `Sessions` |

---

## 🚀 Getting Started

### Prerequisites

Ensure the following are installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the C# extension
- [Git](https://git-scm.com/)

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/Mohameddshawky/GymManagmentSystem.git
cd GymManagmentSystem
```

**2. Restore NuGet packages**

```bash
dotnet restore
```

### Configuration

**3. Set up the connection string**

Open `GymManagmentPL/appsettings.json` and update the `DefaultConnection` string to point to your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=GymManagmentDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> **Tip:** For local development, copy `appsettings.json` values into `appsettings.Development.json` to keep secrets out of source control.

### Database Migration

**4. Apply EF Core migrations**

The application automatically applies pending migrations and seeds initial data on startup. To manually manage migrations, run the following from the solution root:

```bash
dotnet ef database update --project GymManagmentDAL --startup-project GymManagmentPL
```

To add a new migration:

```bash
dotnet ef migrations add <MigrationName> --project GymManagmentDAL --startup-project GymManagmentPL
```

### Run the Application

**5. Start the development server**

```bash
dotnet run --project GymManagmentPL
```

Navigate to `https://localhost:5001` (or the port shown in your terminal). The application will redirect you to the **Login** page. Default seeded admin credentials are configured in `IdentitySeeding.cs`.

---


## 🗺️ Roadmap

- [ ] **REST API Layer** — Expose core functionality via a versioned Web API for mobile client consumption
- [ ] **Role-Based Dashboards** — Separate views and permissions for Admin, Trainer, and Member roles
- [ ] **Payment Integration** — Integrate a payment gateway (e.g., Stripe) for online membership purchases
- [ ] **Notifications** — Email/SMS reminders for expiring memberships and upcoming sessions
- [ ] **Reporting & Analytics** — Visual charts for revenue, attendance trends, and member growth
- [ ] **Member Self-Service Portal** — Allow members to view their own profile, health records, and session bookings
- [ ] **Docker Support** — Containerise the application with a `Dockerfile` and `docker-compose.yml`
- [ ] **Unit & Integration Tests** — Add xUnit test coverage for service and repository layers

---



<div align="center">

Made with ❤️ using **ASP.NET Core 8** · **Entity Framework Core** · **SQL Server**

⭐ If you found this project useful, please consider giving it a star!

</div>
