## HelpdeskSystem


### 📘 Overview
HelpdeskSystem is an enterprise-grade Helpdesk & Ticket Management application built using ASP.NET Core Razor Pages on .NET 8 (LTS).

The project is designed to simulate real-world corporate support systems such as Jira, Zendesk, or ServiceNow.
It focuses strongly on secure authentication, role-based authorization, clean architecture, and workflow-driven ticket management rather than just CRUD operations.

This application demonstrates how a production-ready Razor Pages application should be structured, secured, and scaled, following modern .NET and OOP best practices.

---
<br />



### ✨ Features
✅ User registration & login (ASP.NET Core Identity) <br />
✅ Role-based access (Admin / User) <br />
✅ Secure ticket creation & ownership enforcement <br />
✅ Admin-only ticket visibility & status updates <br />
✅ Dashboard with role-aware metrics <br />
✅ Recent tickets overview (Admin vs User) <br />
✅ Clean UI with responsive Bootstrap layout <br />
✅ Data-level authorization (no data leakage) <br />

---
<br />



### 🔐 Security Standards (Core Focus)
✅ ASP.NET Core Identity authentication <br />
✅ Role-based authorization ([Authorize], roles) <br />
✅ Data-level authorization (user-owned tickets only) <br />
✅ Anti-forgery token protection (CSRF prevention) <br />
✅ Prevention of over-posting attacks <br />
✅ Secure cookies & session handling <br />
✅ Environment-based configuration (appsettings.json) <br />
✅ Server-side & client-side validation <br />
✅ Safe exception handling (no sensitive data leakage) <br />

---
<br />



### 🧠 Modern OOP & Architecture Principles
✅ SOLID principles <br />
✅ Separation of Concerns (UI, Services, Repositories) <br />
✅ Dependency Injection (constructor-based) <br />
✅ Service layer abstraction <br />
✅ Repository pattern <br />
✅ DTOs for data transfer & UI safety <br />
✅ Async/await for scalability <br />
✅ Clean, readable, and maintainable code <br />

---
<br />



### 📊 Dashboard Capabilities
✅ Role-aware dashboard (Admin vs User) <br />
✅ Ticket summary (Total / Open / Resolved) <br />
✅ Admin-only system metrics (Users count)  <br />
✅ Recent tickets overview  <br />
✅ Clean and responsive UI design  <br />

---
<br />



### 🚀 Future Enhancements
✅ Email notifications on ticket updates <br />
✅ File attachments for tickets <br />
✅ Internal & external ticket comments <br />
✅ Ticket priority & SLA management <br />
✅ Real-time updates using SignalR <br />
✅ Advanced reporting & analytics <br />
✅ API layer (ASP.NET Core Web API) <br />
✅ Caching for performance optimization <br />
✅ Docker support & cloud deployment (Azure) <br />
✅ Microservice-ready architecture/> <br />

---
<br />



### 🎯 Key Learnings from this project
✅ Razor Pages architecture & lifecycle <br />
✅ Difference between Web Forms, MVC & Razor Pages <br />
✅ PageModel handlers (OnGet, OnPost) <br />
✅ Model binding & validation <br />
✅ ASP.NET Core Identity internals <br />
✅ Role & policy-based authorization <br />
✅ EF Core Code-First migrations <br />
✅ Async programming with async/await <br />
✅ Folder-based routing in Razor Pages <br />
✅ Clean layering in enterprise .NET apps <br />

---
<br />



### 🛠️ Technologies Used
✅ ASP.NET Core Razor Pages <br />
✅ .NET 8 (LTS) <br />
✅ Entity Framework Core 8 <br />
✅ MS SQL Server <br />
✅ ASP.NET Core Identity <br />
✅ Bootstrap 5 <br />
✅ C# (Modern OOP) <br />

---
<br />



### 🗂️ Project Structure
```
HelpdeskSystem/
│
├── Pages/
│   ├── Dashboard/
│   ├── Tickets/
│   ├── Admin/
│   ├── Account/
│   └── Shared/
│
├── Interfaces/
│   ├── ITicketService.cs
│   ├── ITicketRepository.cs
│
├── Services/
│   └── TicketService.cs
│
├── Repositories/
│   └── TicketRepository.cs
│
├── DTOs/
│   └── DashboardSummaryDto.cs
│
├── Models/
│   ├── Ticket.cs
│   └── TicketStatus.cs
│
├── Data/
│   └── AppDbContext.cs/
│
├── Migration/
├── wwwroot/
├── Program.cs
└── appsettings.json

```

---
<br />



### 📷 UI Screenshots
![Landing Page](https://github.com/Sachin-4-5/HelpdeskSystem/blob/main/Output%20Images/Landing_Page.png)
![Register Page](https://github.com/Sachin-4-5/HelpdeskSystem/blob/main/Output%20Images/Register_Page.png)
![Logged-in User Landing Page](https://github.com/Sachin-4-5/HelpdeskSystem/blob/main/Output%20Images/User_Landing_Page.png)
![Logged-in User Dashboard Page](https://github.com/Sachin-4-5/HelpdeskSystem/blob/main/Output%20Images/User_Dashboard_Page.png)
![Admin Dashboard Page](https://github.com/Sachin-4-5/HelpdeskSystem/blob/main/Output%20Images/Admin_Dashboard_Page.png)
![Admin Setting Page](https://github.com/Sachin-4-5/HelpdeskSystem/blob/main/Output%20Images/Admin_Dashboard_Page2.png)

----
<br />



### ⚙️ How to Clone and Run the Project
```
✅ Prerequisites- .NET 8 SDK, SQL Server, Visual Studio / VS Code
✅ Steps
git clone https://github.com/your-username/HelpdeskSystem.git
cd HelpdeskSystem
dotnet restore
dotnet ef database update
dotnet run

```

---
<br />



 ### 🧪 Default Admin Credentials (Seeded)
Email    : admin@helpdesk.com <br />
Password : Admin@123 <br />
Role     : Admin <br />

---
<br />



### 🤝 Contribution
✅ Contributions are welcome! <br />
✅ Fork the repository <br />
✅ Create a feature branch <br />
✅ Commit your changes <br />
✅ Raise a Pull Request <br />

---
<br >



### 📜 License
This project is licensed under the MIT License.

---
<br />








