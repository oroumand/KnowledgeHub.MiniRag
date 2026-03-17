# ADR-007: Separate AI infrastructure from SQL Server infrastructure

## Status
Accepted

## Context

The project started with a single infrastructure project focused on SQL Server and persistence concerns.

As the implementation progressed, AI-related integrations such as:
- OpenAI embeddings
- future chat completion services
- vector search related services

were introduced.

Keeping these implementations inside the SQL Server infrastructure project would mix unrelated concerns:
- relational persistence
- AI integrations
- external model providers

The project is intended to remain:
- maintainable
- educational
- modular
- open source friendly

## Decision

We will separate infrastructure concerns into dedicated projects.

Current infrastructure projects:

- `KnowledgeHub.MiniRag.Infras.SqlServer`
- `KnowledgeHub.MiniRag.Infras.AI`

Responsibilities:

### KnowledgeHub.MiniRag.Infras.SqlServer
Contains:
- EF Core DbContext
- entity configurations
- relational database persistence
- SQL Server-specific infrastructure

### KnowledgeHub.MiniRag.Infras.AI
Contains:
- OpenAI integration
- embedding service implementations
- future chat completion implementations
- AI-related dependency injection

## Consequences

### Positive

- Better separation of concerns
- Cleaner architecture boundaries
- Easier navigation in the codebase
- Easier future replacement of AI providers
- Better educational value for readers of the open-source project

### Negative

- More projects to manage in the solution
- Slightly more dependency injection setup
- More coordination needed between infrastructure modules

## Alternatives Considered

### Keep all infrastructure in a single project

Pros:
- fewer projects
- simpler setup at the beginning

Cons:
- mixes unrelated concerns
- harder to scale and maintain
- makes the infrastructure project too broad over time

## Notes

This decision aligns with the project's goal of being a clean and understandable sample
application for RAG architecture in .NET.

Additional infrastructure projects may be introduced in the future if new concerns grow enough
to justify independent boundaries.