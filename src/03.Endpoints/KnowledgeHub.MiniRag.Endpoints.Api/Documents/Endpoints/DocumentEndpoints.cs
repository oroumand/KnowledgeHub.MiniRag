using KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;
using KnowledgeHub.MiniRag.Core.Domain.Documents.Enums;
using KnowledgeHub.MiniRag.Endpoints.Api.Documents.Contracts;

namespace KnowledgeHub.MiniRag.Endpoints.Api.Documents.Endpoints;

public static class DocumentEndpoints
{
    public static IEndpointRouteBuilder MapDocumentEndpoints(this IEndpointRouteBuilder app,string path)
    {
        app.MapPost(path, CreateDocumentAsync);

        return app;
    }

    private static async Task<IResult> CreateDocumentAsync(
       CreateDocumentRequest request,
       DocumentIngestionService service,
       CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(new CreateDocumentCommand
        {
            Title = request.Title,
            RawText = request.RawText,
            SourceType = request.SourceType,
            OriginalUrl = request.OriginalUrl,
            Author = request.Author
        }, cancellationToken);

        return Results.Ok(result);
    }
}
