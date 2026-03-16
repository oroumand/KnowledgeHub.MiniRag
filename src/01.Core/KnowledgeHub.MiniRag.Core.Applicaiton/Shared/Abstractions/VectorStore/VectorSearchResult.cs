namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;

public sealed class VectorSearchResult
{
    public string VectorRecordId { get; set; } = null!;

    public Guid ChunkId { get; set; }

    public Guid DocumentId { get; set; }

    public double Score { get; set; }
}
