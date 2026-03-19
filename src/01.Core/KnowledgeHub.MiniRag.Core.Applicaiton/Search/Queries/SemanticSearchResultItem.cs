using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Search.Queries;

public sealed class SemanticSearchResultItem
{
    public Guid DocumentId { get; set; }

    public Guid ChunkId { get; set; }

    public string Title { get; set; } = null!;

    public int ChunkIndex { get; set; }

    public string Text { get; set; } = null!;

    public double Score { get; set; }
}
