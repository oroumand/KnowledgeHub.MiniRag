# ADR-006: Use DbSet directly in Application layer instead of Repository pattern

## Status
Accepted

## Context

In many Clean Architecture implementations, the Application layer interacts with
the persistence layer through repository abstractions.

Example:

- IDocumentRepository
- IChunkRepository

However, for this project we aim to:

- keep the architecture understandable for learners
- avoid unnecessary boilerplate
- move quickly while still keeping architectural boundaries clear
- demonstrate a pragmatic approach used by many modern .NET teams

The project uses EF Core and SQL Server for persistence.

## Decision

Instead of introducing a repository abstraction for every entity,
the Application layer will depend on an abstraction of the EF Core DbContext:

`IApplicationDbContext`

This abstraction exposes the required DbSet properties:

- SourceDocuments
- DocumentChunks

Example:

```csharp
public interface IApplicationDbContext
{
    DbSet<SourceDocument> SourceDocuments { get; }
    DbSet<DocumentChunk> DocumentChunks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
The concrete implementation will live in the Infrastructure layer.

## Consequences
## Positive

Less boilerplate code

Faster development

Easier learning experience

Simpler code navigation

Common approach in modern EF Core projects

## Negative

Application layer has a light dependency on EF Core concepts

Switching ORM would require refactoring

## Alternatives Considered
Repository Pattern

## Pros:

Stronger separation from persistence

Easier ORM replacement

## Cons:

Significant boilerplate

Extra complexity for a learning project

Harder for new developers to follow

## Notes:

If the project evolves into a larger production system,
introducing repositories or a query abstraction layer may become beneficial.