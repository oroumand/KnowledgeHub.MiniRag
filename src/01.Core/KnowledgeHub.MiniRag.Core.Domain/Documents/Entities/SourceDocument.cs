using KnowledgeHub.MiniRag.Core.Domain.Documents.Enums;

namespace KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;

public class SourceDocument
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DocumentSourceType SourceType { get; set; } = DocumentSourceType.Unknown;

    public string? OriginalUrl { get; set; }

    public string? Author { get; set; }

    public string RawText { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; }

    public ICollection<DocumentChunk> Chunks { get; set; } = new List<DocumentChunk>();
}
