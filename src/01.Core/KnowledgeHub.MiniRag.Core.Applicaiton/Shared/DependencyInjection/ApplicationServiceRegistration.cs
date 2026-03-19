using KnowledgeHub.MiniRag.Core.Applicaiton.Chat.Commands;
using KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;
using KnowledgeHub.MiniRag.Core.Applicaiton.Search.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<DocumentIngestionService>();
        services.AddScoped<SemanticSearchService>();
        services.AddScoped<RagChatService>();
        return services;
    }
}