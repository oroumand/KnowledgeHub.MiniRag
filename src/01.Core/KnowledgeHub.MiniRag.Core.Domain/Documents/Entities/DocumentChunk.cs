namespace KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;

public class DocumentChunk
{
    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public SourceDocument Document { get; set; } = null!;

    public int ChunkIndex { get; set; }

    public string Text { get; set; } = null!;

    public int CharacterCount { get; set; }

    public int? EstimatedTokenCount { get; set; }

    public string? VectorRecordId { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}