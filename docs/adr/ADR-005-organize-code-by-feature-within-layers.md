# ADR-005: Organize code by feature within architectural layers

## Status
Accepted

## Context
The project uses a Clean Architecture-inspired separation of concerns, but a purely technical folder structure
can make it harder to navigate related code as the project grows.

The project is intended to be:
- educational
- maintainable
- open source
- easy to evolve over time

## Decision
We will organize code by feature within each architectural layer.

Examples of features include:
- Documents
- Search
- Chat

This means that each layer may contain feature-oriented folders rather than only technical folders.

Examples:
- Domain/Documents/...
- Application/Documents/...
- Endpoints/Documents/...

Shared abstractions and cross-cutting concerns will remain in shared folders where appropriate.

## Consequences

### Positive
- Better discoverability of related code
- Easier maintenance as the project grows
- Better alignment with use cases and product capabilities
- Good fit for modular thinking in a learning-oriented project

### Negative
- Some shared concerns may need careful placement
- Requires discipline to avoid duplicating abstractions across features