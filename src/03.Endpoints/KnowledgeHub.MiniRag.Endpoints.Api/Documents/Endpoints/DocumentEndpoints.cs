using KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.Data;
using KnowledgeHub.MiniRag.Core.Domain.Documents.Enums;
using KnowledgeHub.MiniRag.Endpoints.Api.Documents.Contracts;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.MiniRag.Endpoints.Api.Documents.Endpoints;

public static class DocumentEndpoints
{
    public static IEndpointRouteBuilder MapDocumentEndpoints(this IEndpointRouteBuilder app,string path)
    {
        app.MapPost(path, CreateDocumentAsync);
        app.MapGet(path, GetDocumentsAsync);
        app.MapGet("{path}/{id:guid}", GetDocumentByIdAsync);

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

    private static async Task<IResult> GetDocumentsAsync(
        IApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var documents = await dbContext.SourceDocuments
            .AsNoTracking()
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.SourceType,
                x.Author,
                x.CreatedAtUtc
            })
            .ToListAsync(cancellationToken);

        return Results.Ok(documents);
    }

    private static async Task<IResult> GetDocumentByIdAsync(
        Guid id,
        IApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var document = await dbContext.SourceDocuments
            .AsNoTracking()
            .Include(x => x.Chunks)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (document is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(new
        {
            document.Id,
            document.Title,
            document.SourceType,
            document.Author,
            document.OriginalUrl,
            document.CreatedAtUtc,
            Chunks = document.Chunks
                .OrderBy(x => x.ChunkIndex)
                .Select(x => new
                {
                    x.Id,
                    x.ChunkIndex,
                    x.CharacterCount,
                    x.EstimatedTokenCount,
                    x.VectorRecordId,
                    x.Text
                })
        });
    }
}
