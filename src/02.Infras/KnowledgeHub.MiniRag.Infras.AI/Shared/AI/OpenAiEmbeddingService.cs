using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Embeddings;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeHub.MiniRag.Infras.AI.Shared.AI;

public sealed class OpenAiEmbeddingService : IEmbeddingService
{
    private readonly EmbeddingClient _embeddingClient;
    private readonly ILogger<OpenAiEmbeddingService> _logger;

    public OpenAiEmbeddingService(
       IOptions<OpenAiOptions> options,
       ILogger<OpenAiEmbeddingService> logger)
    {
        _logger = logger;

        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            apiKey = options.Value.ApiKey;
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException(
                "OpenAI API key not found. Set OPENAI_API_KEY or OpenAI:ApiKey.");
        }

        _embeddingClient = new EmbeddingClient(
            options.Value.EmbeddingModel,
            apiKey);
    }
    public async Task<IReadOnlyList<float>> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text cannot be empty.", nameof(text));
        }

        _logger.LogInformation(
            "Generating embedding for text length: {Length}",
            text.Length);

        var result = await _embeddingClient.GenerateEmbeddingAsync(
            text,
            cancellationToken: cancellationToken);

        return result.Value.ToFloats().ToArray();
    }
}
