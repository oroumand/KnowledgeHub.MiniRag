namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;

public interface IChatCompletionService
{
    Task<string> AskAsync(
        string prompt,
        CancellationToken cancellationToken = default);
}