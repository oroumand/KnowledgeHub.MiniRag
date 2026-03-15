# ADR-004: Use OpenAI for embeddings and chat generation

## Status
Accepted

## Context
The project needs a practical and high-quality way to generate embeddings and answers
for a RAG workflow.

## Decision
We will use OpenAI models for:
- embeddings
- chat completion

The initial embedding model will be lightweight and appropriate for a small prototype.

## Consequences
### Positive
- Simple developer experience
- High-quality semantic retrieval support
- Good fit for educational and prototype scenarios

### Negative
- External API dependency
- Usage cost
- Requires API key management