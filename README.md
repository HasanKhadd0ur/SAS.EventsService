# 📌 SAS.EventsService

**SAS.EventsService** is the central backend service responsible for managing and exposing all event-related data. It acts as the single point of interaction for both users and other services like the **Event Detection Service**. Users can view events and manage their areas of interest, while detection services use it to submit new events.

---

## 🌍 Purpose

This service abstracts away the event detection pipeline and provides a clean, efficient API for:

* Retrieving events and their metadata
* Managing user interests
* Linking events to places, topics, and named entities (e.g., "دمشق", "منظمة الصحة العالمية")
* Receiving new events from upstream detection services

---

## 🏗️ Tech Stack

| Layer     | Technology                        |
| --------- | --------------------------------- |
| Framework | ASP.NET Core (Clean Architecture) |
| Language  | C# (.NET 6–9)                     |
| Database  | SQL Server                        |
| CI/CD     | Docker, Jenkins                   |
| Tests     | Unit, Integration, Architecture   |

---

## 🧱 Project Structure

```
src/
├── SAS.EventsService.API/            # Web layer (controllers, endpoints)
├── SAS.EventsService.Application/    # Application logic, use cases, DTOs
├── SAS.EventsService.Domain/         # Core business entities and contracts
├── SAS.EventsService.Infrastructure/
│   ├── Persistence/                  # DB access, repositories
│   └── Services/                     # External service adapters
tests/
├── ArchitectureTests/
├── IntegrationTests/
└── UnitTests/
```

---

## 🧩 Domain Areas

This service is modularized into the following domains, each implemented across multiple layers (Domain, Application, Infrastructure):

* **Events** – core event records (summary, title, timestamp, reviewed flag)
* **User Interests** – user-defined areas of focus
* **Event Topics** – categories/tags associated with events
* **Notifications** – push logic for alerting users
* **Named Entities** – extracted terms like cities, organizations, people

---

## 🔄 Event Lifecycle

1. **Detection Service** sends an event → this service receives it via API.
2. Event is persisted, linked to location (`منطقة`, `مكان`), topic, and named entities.
3. Users can query and subscribe to events by location, topic, etc.
4. Notification rules are triggered for interested users.

---

## 📐 Clean Architecture Overview

This service follows Clean Architecture principles:

### ✅ Domain Layer

Contains:

* Entity definitions
* Repository interfaces
* Custom domain exceptions

Each business domain (events, interests, entities) lives in a separate folder.

### ✅ Application Layer

Contains:

* Use case handlers
* DTOs (Data Transfer Objects)
* Service contracts (e.g., for LLMs, notifications, entity extraction)

### ✅ Infrastructure Layer

Split into:

* `Persistence`: Implements repository interfaces using Entity Framework Core
* `Services`: Implements service contracts (e.g., `IUserService`, external integrations)

### ✅ Presentation Layer

* ASP.NET Controllers define all RESTful endpoints
* Handles API validation, routing, and serialization

---

## ⚙️ Database Schema (Overview)

* `Events`: Main table (title, summary, created date, reviewed)
* `Places`: Geographical locations linked to events
* `Regions`: Broader location context
* `Messages`: Messages associated with the event
* `NamedEntities`: Entities like "دمشق", "WHO" with types (location, org, person)
* `EventTopics`: Thematic labels like "fire", "flood", etc.

---

## 🚀 Getting Started

### Prerequisites

* [.NET 6/7/8/9 SDK](https://dotnet.microsoft.com/)
* SQL Server
* Docker (optional for deployment)

### Run Locally

```bash
dotnet build
dotnet run --project src/SAS.EventsService.API
```

Or with Docker:

```bash
docker-compose up --build
```

---

## 🧪 Testing

### Run All Tests

```bash
dotnet test
```

### Test Types

* **Unit Tests**: Isolated tests for use cases and logic
* **Integration Tests**: Covers full request-to-database scenarios
* **Architecture Tests**: Ensures Clean Architecture rules are followed

---

## 📦 API Endpoints

(Examples)

* `GET /api/events`: List all events
* `POST /api/events`: Add new event (used by detection service)
* `GET /api/interests`: Get user's interest regions/topics
* `POST /api/interests`: Add a new interest

---

## ✅ Key Design Highlights

* Modular domain-oriented structure
* Follows SOLID and Clean Architecture principles
* Easy to test and maintain
* Decoupled from detection logic—purely manages final event state
* Supports rich querying and filtering for consumers

---

## 🧑‍💻 Contributors

This service is part of the broader **SAS** system designed for situational awarenes.