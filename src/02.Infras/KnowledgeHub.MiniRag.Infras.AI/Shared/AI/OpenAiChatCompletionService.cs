using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace KnowledgeHub.MiniRag.Infras.AI.Shared.AI;

public sealed class OpenAiChatCompletionService : IChatCompletionService
{
    private readonly ChatClient _chatClient;

    public OpenAiChatCompletionService(IOptions<OpenAiOptions> options)
    {
        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            apiKey = options.Value.ApiKey;
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("OpenAI API key not found.");
        }

        _chatClient = new ChatClient(options.Value.ChatModel, apiKey);
    }

    public async Task<string> AskAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var completion = await _chatClient.CompleteChatAsync(
            [
                new SystemChatMessage(
                    "You answer only based on the provided context. " +
                    "If the context is insufficient, clearly say you do not know." +
                    "Answer in Persian language for Iranian people"),
                new UserChatMessage(prompt)
            ],
            cancellationToken: cancellationToken);

        return completion.Value.Content[0].Text;
    }
}