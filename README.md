# TaskManager API

A simple task management system built with ASP.NET Core Web API + Blazor UI.


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
- Blazor Server
- HttpClient + System.Text.Json


## Architecture

- Backend: ASP.NET Core Web API
- Frontend: Blazor Server
- Communication: HttpClient + DTO-based API
- State management: Component-based UI state


## Project Structure

- TaskManager.API - REST API for task operations
- TaskManager.UI - Blazor UI application


## Learning Goals

This project was built to practice:
- ASP.NET Core Web API
- Blazor component architecture
- Dependency Injection
- UI state management
- Clean separation between API and UI


# Notes

This is a learning project focused on architecture and fundamentals.
Business logic is intentionally simple.