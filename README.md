# HMS - House Expense Management System

A full-stack application with .NET 10 Web API backend and Blazor WebAssembly frontend for managing house expenses organized by projects.

## Features

- User authentication with JWT tokens
- Project management (CRUD)
- Expense tracking per project
- PostgreSQL database
- Blazor WebAssembly frontend

## Getting Started

### Prerequisites

- .NET 10 SDK
- Docker and Docker Compose
- VS Code (optional)

### Installation

#### Option 1: Run locally

1. Clone the repository
2. Run `dotnet restore`
3. Set up PostgreSQL database (locally or via Docker)
4. Update `src/appsettings.json` with your PostgreSQL connection string
5. Run `dotnet build`
6. Run the API: `cd src && dotnet run`
7. Run the client: `cd Client && dotnet run`

The API will be available at `http://localhost:5280`, client at `http://localhost:5000`.

#### Option 2: Run with Docker Compose

1. Clone the repository
2. Run `docker-compose up --build`

The application will be available at `http://localhost:8080` (API and frontend served together).

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

JWT settings in `src/appsettings.json`.