using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.Data;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Search.Queries;

public sealed class SemanticSearchService
{
    private readonly IEmbeddingService _embeddingService;
    private readonly IApplicationDbContext _dbContext;
    private readonly IVectorStoreService _vectorStoreService;

    public SemanticSearchService(
        IEmbeddingService embeddingService,
        IApplicationDbContext dbContext,
        IVectorStoreService vectorStoreService)
    {
        _embeddingService = embeddingService;
        _dbContext = dbContext;
        _vectorStoreService = vectorStoreService;
    }

    public async Task<IReadOnlyList<SemanticSearchResultItem>> SearchAsync(
        string query,
        int topK = 5,
        CancellationToken cancellationToken = default)
    {
        var queryVector = await _embeddingService.GenerateEmbeddingAsync(
            query,
            cancellationToken);

        var vectorResults = await _vectorStoreService.SearchAsync(
            queryVector,
            topK,
            cancellationToken);

        var chunkIds = vectorResults
            .Select(x => x.ChunkId)
            .Distinct()
            .ToList();

        var chunks = await _dbContext.DocumentChunks
            .AsNoTracking()
            .Include(x => x.Document)
            .Where(x => chunkIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var chunkMap = chunks.ToDictionary(x => x.Id);

        var results = new List<SemanticSearchResultItem>();

        foreach (var vectorResult in vectorResults)
        {
            if (!chunkMap.TryGetValue(vectorResult.ChunkId, out var chunk))
            {
                continue;
            }

            results.Add(new SemanticSearchResultItem
            {
                DocumentId = chunk.DocumentId,
                ChunkId = chunk.Id,
                Title = chunk.Document.Title,
                ChunkIndex = chunk.ChunkIndex,
                Text = chunk.Text,
                Score = vectorResult.Score
            });
        }

        return results;
    }
}