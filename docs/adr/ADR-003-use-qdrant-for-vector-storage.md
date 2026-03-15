# ADR-003: Use Qdrant as vector database for v1

## Status
Accepted

## Context
The project requires semantic search over document chunks using vector embeddings.
For the first version, the solution should be simple to run locally and easy to understand.

## Decision
We will use Qdrant as the vector database for v1 of the project.

## Consequences
### Positive
- Easy local setup with Docker
- Purpose-built for vector search
- Supports metadata payloads
- Good fit for RAG prototypes

### Negative
- Additional infrastructure component to run locally
- Future migration may be needed if deployment strategy changes