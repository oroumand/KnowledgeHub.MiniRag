namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;

public interface IVectorStoreService
{
    Task EnsureCollectionExistsAsync(
        CancellationToken cancellationToken = default);

    Task UpsertAsync(
        UpsertVectorRequest request,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<VectorSearchResult>> SearchAsync(
        IReadOnlyList<float> queryVector,
        int topK,
        CancellationToken cancellationToken = default);
}