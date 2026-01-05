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
- .NET 8 SDK
- PostgreSQL
- Docker (Optional)

## Running the Application

### Option 1: Docker (Recommended)
Run the full stack (Backend + DB + Frontend):
```bash
docker-compose up --build
```
*Note: If you encounter permission errors, try `sudo docker-compose up --build`.*

- **API**: [http://localhost:8080/swagger](http://localhost:8080/swagger)
- **Frontend**: [http://localhost:3000](http://localhost:3000)

### Option 2: Local .NET CLI
1. Configure `appsettings.json` with your PostgreSQL connection string.
2. Run the API:
   ```bash
   dotnet run --project SchoolAs.Api
   ```

## Branches
- `Develop`: Main development branch.
- `Test`: Staging/Verification branch.

## Test Credentials
- **Email**: `test@schoolas.com`
- **Password**: `Password123!`
