using KnowledgeHub.MiniRag.Core.Domain.Documents.Enums;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeHub.MiniRag.Endpoints.Api.Documents.Contracts;

public sealed record CreateDocumentRequest(
    [property: Required]
    [property: MaxLength(300)]
    string Title,

    [property: Required]
    [property: MinLength(20)]
    string RawText,

    DocumentSourceType SourceType = DocumentSourceType.ManualText,

    [property: MaxLength(1000)]
    string? OriginalUrl = null,

    [property: MaxLength(200)]
    string? Author = null
);