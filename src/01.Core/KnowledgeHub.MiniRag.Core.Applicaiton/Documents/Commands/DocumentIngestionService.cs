using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.Data;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;
using KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;

public sealed class DocumentIngestionService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITextChunkingService _chunkingService;
    private readonly IEmbeddingService _embeddingService;
    private readonly IVectorStoreService _vectorStoreService;

    public DocumentIngestionService(
            IApplicationDbContext dbContext,
            ITextChunkingService chunkingService,
            IEmbeddingService embeddingService,
            IVectorStoreService vectorStoreService)
    {
        _dbContext = dbContext;
        _chunkingService = chunkingService;
        _embeddingService = embeddingService;
        _vectorStoreService = vectorStoreService;
    }

    public async Task<CreateDocumentResult> CreateAsync(
       CreateDocumentCommand command,
       CancellationToken cancellationToken = default)
    {
        var document = new SourceDocument
        {
            Id = Guid.NewGuid(),
            Title = command.Title.Trim(),
            RawText = command.RawText.Trim(),
            SourceType = command.SourceType,
            OriginalUrl = command.OriginalUrl,
            Author = command.Author,
            CreatedAtUtc = DateTime.UtcNow
        };

        var chunks = _chunkingService.Chunk(document.RawText);

        var chunkEntities = chunks.Select(chunk =>
        {
            var chunkId = Guid.NewGuid();
            var vectorRecordId = chunkId.ToString("N");

            return new DocumentChunk
            {
                Id = chunkId,
                DocumentId = document.Id,
                ChunkIndex = chunk.ChunkIndex,
                Text = chunk.Text,
                CharacterCount = chunk.CharacterCount,
                EstimatedTokenCount = chunk.EstimatedTokenCount,
                VectorRecordId = vectorRecordId,
                CreatedAtUtc = DateTime.UtcNow
            };
        }).ToList();
        foreach (var chunkEntity in chunkEntities)
        {
            document.Chunks.Add(chunkEntity);
        }

        _dbContext.SourceDocuments.Add(document);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _vectorStoreService.EnsureCollectionExistsAsync(cancellationToken);

        foreach (var chunkEntity in chunkEntities)
        {
            var embedding = await _embeddingService.GenerateEmbeddingAsync(
                chunkEntity.Text,
                cancellationToken);

            await _vectorStoreService.UpsertAsync(new UpsertVectorRequest
            {
                VectorRecordId = chunkEntity.VectorRecordId!,
                Vector = embedding,
                ChunkId = chunkEntity.Id,
                DocumentId = document.Id,
                ChunkIndex = chunkEntity.ChunkIndex,
                Title = document.Title
            }, cancellationToken);
        }

        return new CreateDocumentResult
        {
            DocumentId = document.Id,
            ChunkCount = chunkEntities.Count
        };
    }
}