using KnowledgeHub.MiniRag.Core.Applicaiton.Search.Queries;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Chat.Commands;

public sealed class ChatAnswerResult
{
    public string Answer { get; set; } = null!;

    public IReadOnlyList<SemanticSearchResultItem> Sources { get; set; }
        = Array.Empty<SemanticSearchResultItem>();
}
