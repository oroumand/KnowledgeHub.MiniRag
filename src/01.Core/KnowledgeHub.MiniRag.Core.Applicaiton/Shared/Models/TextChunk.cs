namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Models;

public sealed class TextChunk
{
    public int ChunkIndex { get; set; }

    public string Text { get; set; } = null!;

    public int CharacterCount { get; set; }

    public int EstimatedTokenCount { get; set; }
}