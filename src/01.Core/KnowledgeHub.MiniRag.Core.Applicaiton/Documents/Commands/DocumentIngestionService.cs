using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.Data;
using KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;

public sealed class DocumentIngestionService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITextChunkingService _chunkingService;

    public DocumentIngestionService(
        IApplicationDbContext dbContext,
        ITextChunkingService chunkingService)
    {
        _dbContext = dbContext;
        _chunkingService = chunkingService;
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

        _dbContext.SourceDocuments.Add(document);
        await _dbContext.SaveChangesAsync(cancellationToken);
        

        var chunks = _chunkingService.Chunk(document.RawText);

        foreach (var chunk in chunks)
        {
            var chunkEntity = new DocumentChunk
            {
                Id = Guid.NewGuid(),
                DocumentId = document.Id,
                ChunkIndex = chunk.ChunkIndex,
                Text = chunk.Text,
                CharacterCount = chunk.CharacterCount,
                EstimatedTokenCount = chunk.EstimatedTokenCount,
                CreatedAtUtc = DateTime.UtcNow
            };

            _dbContext.DocumentChunks.Add(chunkEntity);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateDocumentResult
        {
            DocumentId = document.Id,
            ChunkCount = chunks.Count
        };
    }
}