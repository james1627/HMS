# HMS - House Expense Management System

A .NET 8 Web API for managing house expenses organized by projects.

## Features

- User authentication with JWT tokens
- Project management (CRUD)
- Expense tracking per project
- SQLite database

## Getting Started

### Prerequisites

- .NET 8 SDK
- VS Code (optional)

### Installation

1. Clone the repository
2. Navigate to `src` directory
3. Run `dotnet restore`
4. Run `dotnet ef database update` (if migrations not applied)
5. Run `dotnet run`

The API will be available at `https://localhost:5001` (or similar).

### API Endpoints

- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token
- `GET /api/projects` - Get user's projects (requires auth)
- `POST /api/projects` - Create a new project
- `GET /api/expenses` - Get user's expenses (optional projectId query)
- `POST /api/expenses` - Create a new expense

Use the JWT token in Authorization header: `Bearer <token>`

## Database

Uses SQLite. Database file: `HMS.db` in the src directory.

## Configuration

JWT settings in `appsettings.json`.