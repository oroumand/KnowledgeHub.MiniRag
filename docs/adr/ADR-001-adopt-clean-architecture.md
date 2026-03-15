# ADR-001: Adopt Clean Architecture with layered solution structure

## Status
Accepted

## Context
The project aims to be a maintainable and extensible sample RAG application in .NET.
It should remain easy to understand for learning purposes while still following
professional architectural boundaries.

## Decision
We will use a Clean Architecture-inspired layered structure with the following project organization:

- `src/01.Core/KnowledgeHub.MiniRag.Core.Domain`
- `src/01.Core/KnowledgeHub.MiniRag.Core.Application`
- `src/02.Infra/KnowledgeHub.MiniRag.Infras.SqlServer`
- `src/03.Endpoints/KnowledgeHub.MiniRag.Endpoints.Api`

Responsibilities are separated as follows:

- Domain: core business entities and domain concepts
- Application: use cases and abstractions
- Infrastructure: persistence and external service integrations
- Endpoints: HTTP API and request/response orchestration

## Consequences
### Positive
- Clear separation of concerns
- Easier maintenance
- Better testability
- Good educational value for open-source learning

### Negative
- More projects and folders for a small prototype
- Slightly more setup effort at the beginning