# HMS - House Expense Management System

A .NET 10 Web API for managing house expenses organized by projects.

## Features

- User authentication with JWT tokens
- Project management (CRUD)
- Expense tracking per project
- PostgreSQL database

## Getting Started

### Prerequisites

- .NET 10 SDK
- Docker and Docker Compose
- VS Code (optional)

### Installation

#### Option 1: Run locally

1. Clone the repository
2. Navigate to `src` directory
3. Run `dotnet restore`
4. Set up PostgreSQL database (locally or via Docker)
5. Update `appsettings.json` with your PostgreSQL connection string
6. Run `dotnet ef database update` (if migrations not applied)
7. Run `dotnet run`

The API will be available at `http://localhost:5280`.

#### Option 2: Run with Docker Compose

1. Clone the repository
2. Run `docker-compose up --build`

The API will be available at `http://localhost:8080`.

### API Endpoints

- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token
- `GET /api/projects` - Get user's projects (requires auth)
- `POST /api/projects` - Create a new project
- `GET /api/expenses` - Get user's expenses (optional projectId query)
- `POST /api/expenses` - Create a new expense

Use the JWT token in Authorization header: `Bearer <token>`

## Database

Uses PostgreSQL. In Docker setup, it's automatically configured.

## Configuration

JWT settings in `appsettings.json`.