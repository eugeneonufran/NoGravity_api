
# NoGravity API

The **NoGravity API** provides the backend functionality for **NoGravity**, a ticket service for intergalactic travels, enabling futuristic space journeys across the galaxy. Built with a clean architecture and minimal functionality, the API serves as a foundation for managing complex relationships between routes, journeys, carriers, starports, and users in a simulated galaxy.

The frontend for this project can be found here: [NoGravity UI](https://github.com/eugeneonufran/NoGravity_ui).

## Project Overview

The **NoGravity API** is designed to handle core operations such as managing bookings, user data, journey segments, and seating allocations. It is implemented using .NET and follows a modular structure to ensure scalability, maintainability, and high performance. **The project is ongoing and not fully polished, but it provides the minimal required functionality for managing core application operations.**

### Key Features

- **Pathfinding Algorithms**: Utilizes Depth-First Search (DFS) and Dijkstra's algorithm to handle route-finding efficiently.
- **RESTful API Design**: Consistent and well-structured endpoints to support all major CRUD operations.
- **Repository Pattern**: Centralized data access through repositories, enabling a clear separation between data and business logic.
- **DTOs (Data Transfer Objects)**: Ensures efficient data handling across the application and API endpoints.
- **Modular Structure**: Organized into controllers, services, and repositories, allowing for easy testing and maintenance.

## Getting Started

### Prerequisites

- .NET SDK 6.0+
- SQL Server (or a compatible database)

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/eugeneonufran/NoGravity_api.git
   cd NoGravityAPI
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Database Setup**:
   - Configure your connection string in `appsettings.json`.
   - Run migrations to set up the database schema:
     ```bash
     dotnet ef database update
     ```

4. **Run the API**:
   ```bash
   dotnet run
   ```

### Usage

Access the API locally at `http://localhost:5000` with endpoints structured as follows:

- **/api/booking** - Manages bookings and reservations.
- **/api/carriers** - Handles data related to space carriers.
- **/api/journeys** - Manages journey-related data.
- **/api/segments** - Manages individual segments within journeys.
- **/api/planets** - Provides data about planets in the galaxy.
- **/api/allocations** - Manages seating allocations for journeys.
- **/api/starports** - Manages starport data.
- **/api/tickets** - Handles ticketing operations.
- **/api/users** - Manages user profiles and authentication.

### Key Components

#### Controllers

- **BookingController**: Manages bookings and reservations.
- **CarriersController**: Handles CRUD operations for carriers.
- **JourneysController**: Manages journeys including pathfinding and segments.
- **SeatAllocationsController**: Manages seating for journeys.
- **UsersController**: Handles user registration, authentication, and profile management.

#### Models and DTOs

- **JourneyDTO, JourneySegmentDTO**: Represents journey and segment data, ensuring efficient data handling.
- **CarrierDTO, UserDTO**: Simplified representations of core models for external data sharing.
- **RouteDTO, RouteSegmentDTO**: Manages route details for interplanetary travel.
- **TicketDTO**: Facilitates ticket management within journeys.

#### Repository Layer

Each repository (e.g., `JourneysRepository`, `UsersRepository`) provides standardized data access methods, encapsulating database interactions to maintain a clean architecture.

#### Core Algorithms

- **RouteFinder**: Uses DFS and Dijkstra's algorithm to determine the most efficient path through a series of interconnected nodes, simulating interplanetary travel routes.

## Technology Stack

- **Language**: C# (.NET 6)
- **Framework**: ASP.NET Core
- **Database**: Entity Framework with SQL Server
- **Architecture**: Clean Architecture with Repository and Service layers
- **Tools**: Swagger for API documentation, Dependency Injection, and AutoMapper for mapping entities and DTOs

## Future Enhancements

This minimal implementation serves as a foundation. Future plans include:

- Advanced user management (OAuth2 or JWT for authentication)
- Integration with third-party services for dynamic galaxy data
- Enhanced pathfinding with additional algorithms for more complex route planning
- Comprehensive unit and integration testing

## License

This project is licensed under the MIT License.
