using KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<SourceDocument> SourceDocuments { get; }

    DbSet<DocumentChunk> DocumentChunks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}