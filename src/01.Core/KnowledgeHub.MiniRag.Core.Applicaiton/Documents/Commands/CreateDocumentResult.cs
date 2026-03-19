namespace KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;

public sealed class CreateDocumentResult
{
    public Guid DocumentId { get; set; }

    public int ChunkCount { get; set; }
}