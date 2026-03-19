using KnowledgeHub.MiniRag.Core.Applicaiton.Chat.Commands;

namespace KnowledgeHub.MiniRag.Endpoints.Api.Chat.Endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder MapChatEndpoints(this IEndpointRouteBuilder app,string pathPrefix)
    {
        app.MapPost("{pathPrefix}/ask", AskAsync);

        return app;
    }

    private static async Task<IResult> AskAsync(
        AskChatRequest request,
        RagChatService service,
        CancellationToken cancellationToken)
    {
        var result = await service.AskAsync(
            request.Question,
            request.TopK,
            cancellationToken);

        return Results.Ok(result);
    }
}