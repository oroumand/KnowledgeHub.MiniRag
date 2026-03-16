namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;

public sealed class UpsertVectorRequest
{
    public string VectorRecordId { get; set; } = null!;

    public IReadOnlyList<float> Vector { get; set; } = Array.Empty<float>();

    public Guid ChunkId { get; set; }

    public Guid DocumentId { get; set; }

    public int ChunkIndex { get; set; }

    public string Title { get; set; } = null!;
}