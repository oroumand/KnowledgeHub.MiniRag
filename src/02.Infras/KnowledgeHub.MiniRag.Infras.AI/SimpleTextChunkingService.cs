using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Models;
using System.Text.RegularExpressions;

namespace KnowledgeHub.MiniRag.Infras.AI;

public sealed class SimpleTextChunkingService : ITextChunkingService
{
    private const int MaxCharactersPerChunk = 2000;
    private const int ApproxCharsPerToken = 4;


    public IReadOnlyList<TextChunk> Chunk(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Array.Empty<TextChunk>();
        }

        var paragraphs = Regex
            .Split(text.Trim(), @"(\r?\n){2,}")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .ToList();

        var chunks = new List<TextChunk>();
        var currentParts = new List<string>();
        var currentLength = 0;
        var chunkIndex = 0;

        foreach (var paragraph in paragraphs)
        {
            var candidateLength = currentLength == 0
                ? paragraph.Length
                : currentLength + Environment.NewLine.Length * 2 + paragraph.Length;

            if (candidateLength > MaxCharactersPerChunk && currentParts.Count > 0)
            {
                var chunkText = string.Join(Environment.NewLine + Environment.NewLine, currentParts);

                chunks.Add(new TextChunk
                {
                    ChunkIndex = chunkIndex++,
                    Text = chunkText,
                    CharacterCount = chunkText.Length,
                    EstimatedTokenCount = EstimateTokens(chunkText)
                });

                currentParts.Clear();
                currentLength = 0;
            }

            currentParts.Add(paragraph);
            currentLength = currentLength == 0
                ? paragraph.Length
                : currentLength + Environment.NewLine.Length * 2 + paragraph.Length;
        }
        if (currentParts.Count > 0)
        {
            var chunkText = string.Join(Environment.NewLine + Environment.NewLine, currentParts);

            chunks.Add(new TextChunk
            {
                ChunkIndex = chunkIndex,
                Text = chunkText,
                CharacterCount = chunkText.Length,
                EstimatedTokenCount = EstimateTokens(chunkText)
            });
        }
        return chunks;
    }
    private static int EstimateTokens(string text)
    {
        return Math.Max(1, text.Length / ApproxCharsPerToken);
    }
}
