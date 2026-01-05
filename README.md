# School_AS Backend API

## Overview
This is the Backend API for the School_AS course management platform. It is built with **.NET 8** using **Clean Architecture** principles.

## Features
- **Clean Architecture**: Domain, Application, Infrastructure, API.
- **Entity Framework Core**: Code-First approach with generic repositories.
- **Identity**: JWT Authentication.
- **Soft Delete**: Implemented via Global Query Filters.
- **Unit Tests**: Business logic validation using xUnit and Moq.
- **Docker**: Full containerization support.

## Prerequisites
- .NET 8 SDK
- PostgreSQL (or use Docker)

## Configuration
The `appsettings.json` is configured for local development.
- **Database**: PostgreSQL (User: `postgres`, Pass: `password`, DB: `SchoolAsDb`)
- **JWT**: Pre-configured secret for development.

## Running the Application

### Option 1: Docker (Recommended)
Run the entire stack (Backend + Database + Frontend) from the root directory:
```bash
docker-compose up --build
```
API will be available at: http://localhost:8080/swagger

### Option 2: Local .NET CLI
1. Update connection string in `appsettings.json` if needed.
2. Run migrations (if not auto-applied):
   ```bash
   dotnet ef database update --project SchoolAs.Infrastructure --startup-project SchoolAs.Api
   ```
3. Run the API:
   ```bash
   dotnet run --project SchoolAs.Api
   ```

## Test Credentials
A default user is seeded on startup:
- **Email**: `test@schoolas.com`
- **Password**: `Password123!`

## Project Structure
- **SchoolAs.Domain**: Entities, Enums, Interfaces (Core logic).
- **SchoolAs.Application**: Services, DTOs, Business Rules.
- **SchoolAs.Infrastructure**: EF Core, Repositories, Migrations.
- **SchoolAs.Api**: Controllers, Auth Configuration, DI.
- **SchoolAs.UnitTests**: xUnit tests for business logic.

## Deployment
- **Render**: Use `render.yaml` for blueprint deployment.
