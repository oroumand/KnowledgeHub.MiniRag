using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Models;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;

public interface ITextChunkingService
{
    IReadOnlyList<TextChunk> Chunk(string text);
}
