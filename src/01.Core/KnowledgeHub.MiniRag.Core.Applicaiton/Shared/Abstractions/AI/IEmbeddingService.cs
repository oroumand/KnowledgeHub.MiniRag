namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;

public interface IEmbeddingService
{
    Task<IReadOnlyList<float>> GenerateEmbeddingAsync(
        string text,
        CancellationToken cancellationToken = default);
}
