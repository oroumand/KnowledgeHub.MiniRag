using KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<DocumentIngestionService>();

        return services;
    }
}