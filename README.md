# Flag Explorer App

Flag Explorer App is a full-stack application built with a .NET 8 Web API backend and a React + Vite frontend. It retrieves country data from the Rest Countries API and displays a home grid plus a details view for each country.

---

## Tech Stack

- Backend: ASP.NET Core Web API (.NET 8)
- Frontend: React + Vite + TypeScript
- Data fetching: TanStack Query + Axios
- Testing: xUnit + Vitest
- CI/CD: GitHub Actions

---

## Prerequisites

Before starting, make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (18 or newer recommended)
- npm
- Git

---

## Local setup

### 1. Clone the repository

```bash
git clone https://github.com/wondermkhize/FlagExplorerApp.git
cd FlagExplorerApp
```

### 2. Restore .NET dependencies

```bash
dotnet restore
```

### 3. Run the backend

```bash
cd API
dotnet watch run
```

The API will run at:

- http://localhost:5000
- https://localhost:5001

Swagger is available at:

- http://localhost:5000/swagger
- https://localhost:5001/swagger

### 4. Install frontend dependencies

```bash
cd client
npm install
```

### 5. Configure the frontend API URL

Copy the example environment file and update the value if needed:

```bash
copy .env.example .env
```

Example `.env`:

```env
VITE_API_BASE_URL=http://localhost:5000/api
```

### 6. Run the frontend

```bash
npm run dev
```

The frontend will be available at:

- http://localhost:5173

---

## Run tests

### Backend

From the project root:

```bash
dotnet test
```

### Frontend

From the client folder:

```bash
npm run test -- --run
```

### Linting

```bash
npm run lint
```

---

## CI/CD

The GitHub Actions workflow in [.github/workflows/ci-cd.yml](.github/workflows/ci-cd.yml) runs:

- frontend lint
- frontend tests
- frontend build
- backend restore/build
- backend tests
- artifact packaging

---

## Project structure

- `API/` – ASP.NET Core API project
- `Infrastructure/` – service and infrastructure layer
- `BackEndTests/` – backend tests
- `client/` – React frontend
