using KnowledgeHub.MiniRag.Core.Domain.Documents.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;

public sealed class CreateDocumentCommand
{
    public string Title { get; set; } = null!;

    public string RawText { get; set; } = null!;

    public DocumentSourceType SourceType { get; set; } = DocumentSourceType.ManualText;

    public string? OriginalUrl { get; set; }

    public string? Author { get; set; }
}
