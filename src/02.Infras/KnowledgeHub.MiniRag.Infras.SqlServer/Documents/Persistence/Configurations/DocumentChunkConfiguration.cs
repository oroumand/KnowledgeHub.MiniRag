using KnowledgeHub.MiniRag.Core.Domain.Documents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeHub.MiniRag.Infras.SqlServer.Documents.Persistence.Configurations;

public partial class SourceDocumentConfiguration
{
    public class DocumentChunkConfiguration : IEntityTypeConfiguration<DocumentChunk>
    {
        public void Configure(EntityTypeBuilder<DocumentChunk> builder)
        {
            builder.ToTable("DocumentChunks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .IsRequired();

            builder.Property(x => x.VectorRecordId)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedAtUtc)
                .IsRequired();

            builder.HasIndex(x => x.DocumentId);

            builder.HasIndex(x => x.VectorRecordId);

            builder.HasIndex(x => new { x.DocumentId, x.ChunkIndex })
                .IsUnique();
        }
    }
}
