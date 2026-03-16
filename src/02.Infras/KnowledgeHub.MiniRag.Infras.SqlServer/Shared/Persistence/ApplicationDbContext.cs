using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.Data;
using KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.MiniRag.Infras.SqlServer.Shared.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<SourceDocument> SourceDocuments => Set<SourceDocument>();

    public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
