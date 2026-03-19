using System.Text.Json.Serialization;

namespace KnowledgeHub.MiniRag.Infras.AI.Shared.VectorStore;

internal sealed class QdrantCreateCollectionRequest
{
    [JsonPropertyName("vectors")]
    public QdrantVectorConfig Vectors { get; set; } = new();
}

internal sealed class QdrantVectorConfig
{
    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("distance")]
    public string Distance { get; set; } = "Cosine";
}

internal sealed class QdrantUpsertRequest
{
    [JsonPropertyName("points")]
    public List<QdrantPoint> Points { get; set; } = new();
}

internal sealed class QdrantPoint
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("vector")]
    public IReadOnlyList<float> Vector { get; set; } = Array.Empty<float>();

    [JsonPropertyName("payload")]
    public Dictionary<string, object> Payload { get; set; } = new();
}

internal sealed class QdrantSearchRequest
{
    [JsonPropertyName("vector")]
    public IReadOnlyList<float> Vector { get; set; } = Array.Empty<float>();

    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("with_payload")]
    public bool WithPayload { get; set; } = true;
}
internal sealed class QdrantSearchResponse
{
    [JsonPropertyName("result")]
    public List<QdrantSearchItem> Result { get; set; } = new();
}

internal sealed class QdrantSearchItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("payload")]
    public Dictionary<string, object>? Payload { get; set; }
}