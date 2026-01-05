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
   - URL: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   - *Note: mapped to port 5000 on host.*

### Option 2: Local .NET CLI
1. Configure `appsettings.json` with your PostgreSQL connection string.
2. Run the API:
   ```bash
   dotnet run --project SchoolAs.Api
   ```
   (Default port usually 8080 or 5000 check console output).

## Connecting the Frontend
To use the UI, clone the [Frontend Repository](https://github.com/Mylisuthy/School_AS_Frontend.git) and run it locally, pointing it to `http://localhost:5000/api`.

## Branches
- `Develop`: Main development branch.
- `Test`: Staging/Verification branch.

## Test Credentials
- **Email**: `test@schoolas.com`
- **Password**: `Password123!`
