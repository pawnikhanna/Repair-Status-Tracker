# Repair Status Tracker

A full-stack application for viewing and updating vehicle repair job statuses.

**Stack:** ASP.NET Core 8 (C#) · React 18 + TypeScript (Vite)

---

## Project Structure

```
RepairStatusTracker/
├── RepairTracker.Api/        # ASP.NET Core Web API
└── repair-tracker-ui/        # React + TypeScript frontend
```

---

## Running the API

**Prerequisites:** .NET 8 SDK

```bash
cd RepairTracker.Api
dotnet run
```

The API starts on `http://localhost:5090`.

### Endpoints

| Method | URL | Description |
|--------|-----|-------------|
| GET | `/api/repairjobs` | Return all repair jobs |
| PATCH | `/api/repairjobs/{id}/status` | Update a job's status |

---

## Running the Frontend

**Prerequisites:** Node.js 18+

```bash
cd repair-tracker-ui
npm install
npm run dev
```

The UI starts on `http://localhost:5173`.

> Make sure the API is running before starting the frontend.

---

## Assumptions

- Data is stored in-memory using a thread-safe `ConcurrentDictionary` and resets on API restart. This handles potential multi-user concurrent updates across locations without data corruption, reduces evaluation setup friction, and can be swapped for EF Core + SQL Server with a single-line modification because of the decoupled `IRepairJobRepository` interface.
- Status values are validated server-side in the service layer. Passing an unapproved status returns a `400 Bad Request` with a descriptive message.

---

## What I Would Improve With More Time

1. **Persistence** — swap `InMemoryJobRepository` for an EF Core implementation backed by SQL Server, using the existing `IJobRepository` interface. No changes required in the service or controller layer.
2. **Containerization** — add a `Dockerfile` for each service and a `docker-compose.yml` to wire them together, which also sets up a natural path to Azure Container Apps deployment.
3. **Unit tests** — add xUnit tests for `JobService` (especially status validation logic) and integration tests for the controller endpoints using `WebApplicationFactory`.

---

## AI Usage

I used Gemini (Google) as a development collaborator during this project.

- **What AI was used for:** Generating the data schemas and realistic mock data arrays matching the Crash Champions scope, scaffolding UI layout boilerplate, and refining cross-origin resource sharing (CORS) setup code.
- **What I reviewed and modified manually:** All structural design decisions (Controller-Service-Repository boundaries), strong-typed validation guards inside RepairJobService, strict TypeScript interface models, configuring verbatimModuleSyntax and erasableSyntaxOnly compiler compliance criteria, and row-level lookup mappings.
