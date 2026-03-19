using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.AI;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;
using KnowledgeHub.MiniRag.Infras.AI.Shared.AI;
using KnowledgeHub.MiniRag.Infras.AI.Shared.VectorStore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeHub.MiniRag.Infras.AI.Shared.DependencyInjection;

public static class AIServiceRegistration
{
    public static IServiceCollection AddAI(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ITextChunkingService, SimpleTextChunkingService>();

        services.Configure<OpenAiOptions>(
          configuration.GetSection(OpenAiOptions.SectionName));

        services.AddScoped<IEmbeddingService, OpenAiEmbeddingService>();
        services.AddScoped<IChatCompletionService, OpenAiChatCompletionService>();

        services.Configure<QdrantOptions>(
           configuration.GetSection(QdrantOptions.SectionName));
        services.AddHttpClient<IVectorStoreService, QdrantVectorStoreService>();

        return services;
    }
}