# ADR-008: Allow direct read-only queries in Endpoints for debug inspection endpoints

## Status
Accepted (Temporary)

## Context

The project currently includes inspection endpoints used for development-time debugging and verification of the ingestion pipeline.

Examples include:
- listing documents
- retrieving document details
- viewing generated chunks

These endpoints are primarily intended to help developers verify:
- document persistence
- chunk generation
- vector record assignment

At this stage of the project, the priority is fast feedback and visibility into the ingestion pipeline.

## Decision

For the initial development phase, simple read-only inspection queries may access
`IApplicationDbContext` directly from the Endpoints layer.

This is allowed only for:
- temporary development/debug endpoints
- simple read-only inspection scenarios

Business workflows and reusable use cases should continue to be implemented in the Application layer.

## Consequences

### Positive

- Faster development
- Easier debugging of ingestion
- Less boilerplate during early project stages
- Quick visibility into persisted data

### Negative

- Some query logic exists outside the Application layer
- Architectural consistency is weakened
- Endpoints become aware of EF Core query details
- Future refactoring is likely needed

## Alternatives Considered

### Move all read queries to Application immediately

Pros:
- Cleaner architecture
- Better consistency
- Better long-term maintainability

Cons:
- More setup and boilerplate during early development
- Slower iteration while core pipeline is still evolving

## Notes

This is a temporary decision.
As the project matures, inspection queries should be moved into the Application layer
as dedicated query use cases.