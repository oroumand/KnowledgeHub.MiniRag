# ADR-002: Use SQL Server as primary relational store

## Status
Accepted

## Context
The project needs a reliable primary data store for documents, chunks, and metadata.
The stack is based on .NET and EF Core.

## Decision
We will use SQL Server as the primary relational database and EF Core as the ORM.

## Consequences
### Positive
- Strong integration with .NET ecosystem
- Good tooling support
- Familiar development workflow
- Reliable storage for structured application data

### Negative
- Requires local SQL Server setup for development
- Adds operational dependency compared to file-based storage