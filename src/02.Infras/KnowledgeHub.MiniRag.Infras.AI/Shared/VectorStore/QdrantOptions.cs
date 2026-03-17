namespace KnowledgeHub.MiniRag.Infras.AI.Shared.VectorStore;

public sealed class QdrantOptions
{
    public const string SectionName = "Qdrant";

    public string BaseUrl { get; set; } = "http://localhost:6333";

    public string CollectionName { get; set; } = "document_chunks";
}
