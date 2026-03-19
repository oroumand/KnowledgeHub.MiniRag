using KnowledgeHub.MiniRag.Core.Applicaiton.Search.Queries;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using System.Text;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Chat.Commands;

public sealed class RagChatService
{
    private readonly SemanticSearchService _semanticSearchService;
    private readonly IChatCompletionService _chatCompletionService;

    public RagChatService(
        SemanticSearchService semanticSearchService,
        IChatCompletionService chatCompletionService)
    {
        _semanticSearchService = semanticSearchService;
        _chatCompletionService = chatCompletionService;
    }

    public async Task<ChatAnswerResult> AskAsync(
        string question,
        int topK = 5,
        CancellationToken cancellationToken = default)
    {
        var sources = await _semanticSearchService.SearchAsync(
            question,
            topK,
            cancellationToken);

        var prompt = BuildPrompt(question, sources);

        var answer = await _chatCompletionService.AskAsync(
            prompt,
            cancellationToken);

        return new ChatAnswerResult
        {
            Answer = answer,
            Sources = sources
        };
    }

    private static string BuildPrompt(
        string question,
        IReadOnlyList<SemanticSearchResultItem> sources)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Answer the user's question only using the context below.");
        sb.AppendLine("If the answer cannot be determined from the context, say that you do not know.");
        sb.AppendLine();
        sb.AppendLine("Context:");

        foreach (var source in sources)
        {
            sb.AppendLine($"[Source: {source.Title} | Chunk: {source.ChunkIndex}]");
            sb.AppendLine(source.Text);
            sb.AppendLine();
        }

        sb.AppendLine("User Question:");
        sb.AppendLine(question);

        return sb.ToString();
    }
}