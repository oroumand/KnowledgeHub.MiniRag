using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.Abstractions.VectorStore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace KnowledgeHub.MiniRag.Infras.AI.Shared.VectorStore;

public sealed class QdrantVectorStoreService : IVectorStoreService
{
    private readonly HttpClient _httpClient;
    private readonly QdrantOptions _options;
    private readonly ILogger<QdrantVectorStoreService> _logger;

    private readonly string _searchEndpointUri;
    private readonly string _upsertEndpointUri;
    private readonly string _collectionEndpointUri;

    private const string CHUNK_ID_KEY = "chunkId";//documentId,chunkIndex,title
    private const string CHUNK_INDEX_KEY = "chunkIndex";//documentId,,title
    private const string DOCUMENT_ID_KEY = "documentId";//documentId,chunkIndex,title
    private const string TITLE_KEY = "title";//documentId,chunkIndex,title
    private const string DISTANCE_CALCULATION_ALGORITHM = "Cosine";
    private const int VECTOR_SIZE = 1536;
    public QdrantVectorStoreService(
        HttpClient httpClient,
        IOptions<QdrantOptions> options,
        ILogger<QdrantVectorStoreService> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;



        _httpClient.BaseAddress = new Uri(_options.BaseUrl);

        _collectionEndpointUri = $"/collections/{_options.CollectionName}";
        _upsertEndpointUri = $"{_collectionEndpointUri}/points";
        _searchEndpointUri = $"{_upsertEndpointUri}/search";
    }
    public async Task EnsureCollectionExistsAsync(
           CancellationToken cancellationToken = default)
    {
        var getResponse = await _httpClient.GetAsync(
            _collectionEndpointUri,
            cancellationToken);

        if (getResponse.IsSuccessStatusCode)
        {
            return;
        }

        if (getResponse.StatusCode != HttpStatusCode.NotFound)
        {
            var errorText = await getResponse.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException(
                $"Failed to check Qdrant collection. {errorText}");
        }

        const int vectorSize = VECTOR_SIZE;

        var request = new QdrantCreateCollectionRequest
        {
            Vectors = new QdrantVectorConfig
            {
                Size = vectorSize,
                Distance = DISTANCE_CALCULATION_ALGORITHM
            }
        };

        _logger.LogInformation(
            "Creating Qdrant collection {CollectionName}",
            _options.CollectionName);

        var createResponse = await _httpClient.PutAsJsonAsync(
            _collectionEndpointUri,
            request,
            cancellationToken);

        createResponse.EnsureSuccessStatusCode();
    }

    public async Task UpsertAsync(
            UpsertVectorRequest request,
            CancellationToken cancellationToken = default)
    {
        var payload = new Dictionary<string, object>
        {
            [CHUNK_ID_KEY] = request.ChunkId,
            [DOCUMENT_ID_KEY] = request.DocumentId,
            [CHUNK_INDEX_KEY] = request.ChunkIndex,
            [TITLE_KEY] = request.Title
        };

        var body = new QdrantUpsertRequest
        {
            Points =
            [
                new QdrantPoint
                {
                    Id = request.VectorRecordId,
                    Vector = request.Vector,
                    Payload = payload
                }
            ]
        };

        var response = await _httpClient.PutAsJsonAsync(
            _upsertEndpointUri,
            body,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }


    public async Task<IReadOnlyList<VectorSearchResult>> SearchAsync(
            IReadOnlyList<float> queryVector,
            int topK,
            CancellationToken cancellationToken = default)
    {
        var body = new QdrantSearchRequest
        {
            Vector = queryVector,
            Limit = topK
        };

        var response = await _httpClient.PostAsJsonAsync(
            _searchEndpointUri,
            body,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var searchResponse = await response.Content.ReadFromJsonAsync<QdrantSearchResponse>(
            cancellationToken: cancellationToken)
            ?? new QdrantSearchResponse();

        var results = new List<VectorSearchResult>();

        foreach (var item in searchResponse.Result)
        {
            if (item.Payload is null)
            {
                continue;
            }

            var chunkId = ReadGuid(item.Payload, CHUNK_ID_KEY);
            var documentId = ReadGuid(item.Payload, DOCUMENT_ID_KEY);

            results.Add(new VectorSearchResult
            {
                VectorRecordId = item.Id,
                ChunkId = chunkId,
                DocumentId = documentId,
                Score = item.Score
            });
        }

        return results;
    }

    private static Guid ReadGuid(
        Dictionary<string, object> payload,
        string key)
    {
        if (!payload.TryGetValue(key, out var value))
        {
            return Guid.Empty;
        }

        if (value is JsonElement json && json.ValueKind == JsonValueKind.String)
        {
            return Guid.Parse(json.GetString()!);
        }

        return Guid.Parse(value.ToString()!);
    }
}
