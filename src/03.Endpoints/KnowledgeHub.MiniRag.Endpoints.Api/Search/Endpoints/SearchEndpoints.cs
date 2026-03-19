using KnowledgeHub.MiniRag.Core.Applicaiton.Search.Queries;
using KnowledgeHub.MiniRag.Endpoints.Api.Search.Contracts;

namespace KnowledgeHub.MiniRag.Endpoints.Api.Search.Endpoints;

public static class SearchEndpoints
{

    public static IEndpointRouteBuilder MapSearchEndpoints(this IEndpointRouteBuilder app, string pathPrefix)
    {
        app.MapPost("{pathPrefix}/semantic", SearchAsync);

        return app;
    }

    private static async Task<IResult> SearchAsync(
        SemanticSearchRequest request,
        SemanticSearchService service,
        CancellationToken cancellationToken)
    {
        var result = await service.SearchAsync(
            request.Query,
            request.TopK,
            cancellationToken);

        return Results.Ok(result);
    }
}