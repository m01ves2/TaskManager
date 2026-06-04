# TaskManager API

A simple Task Management system built with ASP.NET Core, EF Core, and Clean Architecture principles.

## Features

- Create, update, delete tasks
- Mark tasks as completed
- Soft delete and restore tasks
- Pagination, filtering, sorting
- Centralized error handling via middleware
- Swagger API documentation

## Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

## Architecture

- Controller - Service - Repository pattern
- Business logic in Service layer
- Data access abstracted via Repository
- Global exception handling via middleware

## API

Available via Swagger:
/swagger

## Status

Project is under active development (learning project).
UI layer will be added next.