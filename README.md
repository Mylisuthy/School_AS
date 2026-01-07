# School_AS Backend API

## Overview
This is the Backend API for the School_AS course management platform. It is built with **.NET 8** using **Clean Architecture** principles.

## Repositories
- **Backend (This Repo)**: [https://github.com/Mylisuthy/School_AS.git](https://github.com/Mylisuthy/School_AS.git)
- **Frontend**: [https://github.com/Mylisuthy/School_AS_Frontend.git](https://github.com/Mylisuthy/School_AS_Frontend.git)

## Features
- **Clean Architecture**: Domain, Application, Infrastructure, API.
- **Entity Framework Core**: Code-First with generic repositories.
- **Identity**: JWT Authentication.
- **Soft Delete**: Global Query Filters.
- **Unit Tests**: 5/5 Requirements Met.
- **Data Seeding**: Test user (`test@schoolas.com`) created on startup.

## Prerequisites
- Docker (Recommended)
- OR .NET 8 SDK + PostgreSQL

## Running the Application

### Option 1: Docker (Recommended)
This repository contains the `docker-compose` setup for the **Backend API** and **Database**.

1. Run the stack:
   ```bash
   sudo docker-compose up --build
   ```
2. Access the API Documentation:
   - URL: [http://localhost:15000/swagger](http://localhost:15000/swagger)
   - *Note: mapped to port 15000 on host.*

## Configuration & Database

### Database Connection
- **Docker**: Automatically configured via environment variables in `docker-compose.yml`.
- **Local Development**: Update `appsettings.json` with your PostgreSQL connection string:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=15432;Database=school_as;Username=postgres;Password=your_password;Ssl Mode=Disable"
  }
  ```

### Migrations
The application is configured to **automatically assign migrations** on startup (`Program.cs`).
To run them manually:
```bash
dotnet ef database update --project SchoolAs.Infrastructure --startup-project SchoolAs.Api
```

## Running the Project

### Option 1: Docker (Recommended) 🐳
The easiest way to run the full stack (API + Database + Frontend).
```bash
docker-compose up --build
```
- **Frontend**: [http://localhost:13000](http://localhost:13000)
- **Backend API**: [http://localhost:15000/swagger](http://localhost:15000/swagger)

### Option 2: Manual Run
1. Start PostgreSQL.
2. Run Backend: `dotnet run --project SchoolAs.Api`
3. Run Frontend: `npm run dev` (in `SchoolAs.Web`)

## Test Credentials 🔑

The system automatically creates these users on startup:

| Role | Email | Password |
|------|-------|----------|
| **Admin** | `test@schoolas.com` | `Password123!` |
| **Student** | `student@schoolas.com` | `Password123!` |
