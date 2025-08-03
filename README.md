 ---Project Management API (.NET 8)------
Features & Functionalities:

User Roles & Authentication
Login system using JWT (JSON Web Tokens).

Secure access based on roles:
-Admin: Full access to users, projects, tasks.
-Project Manager: Can manage projects and tasks.
-Developer: Can view and update assigned tasks.
-Viewer: Read-only access.

User password hashed securely using BCrypt.

Token generation during login and token-based access for all APIs.

✅ CRUD Operations
👤 User Management (/api/users)
Admin can:

View all users

Assign or update roles

Any user can view their own profile

🔐 Authentication (/api/auth)
Login via email + password → returns JWT token

Admin can register new users via /register

📁 Project Management (/api/project)
Admin & PM can:
Create, update, and delete projects

All users can:
View project list and details

Task Management (/api/task)
Tasks are linked to a project

PM and Developers can:
Create, update, and delete tasks

All users can:
View tasks by project

🔐 Authorization Policies
Custom policies implemented:
RequireAdmin → Admin only
PMOrAdmin → PM or Admin
DevOrAbove → Developer, PM, or Admin

Tech Stack Used: 
- .NET 8 Web API
- SQL Server
- JWT Authentication
- Role-Based Authorization
- Swagger
- EF Core

Setup

1. Clone the repo
2. Restore NuGet packages
3. Update connection string in `appsettings.json`
4. Run migration:
