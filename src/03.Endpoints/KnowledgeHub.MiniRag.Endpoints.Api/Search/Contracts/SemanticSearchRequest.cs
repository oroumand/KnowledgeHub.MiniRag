using System.ComponentModel.DataAnnotations;

namespace KnowledgeHub.MiniRag.Endpoints.Api.Search.Contracts;

public sealed record SemanticSearchRequest(
    [property: Required]
    [property: MinLength(3)]
    string Query,

    [property: Range(1, 20)]
    int TopK = 5
);