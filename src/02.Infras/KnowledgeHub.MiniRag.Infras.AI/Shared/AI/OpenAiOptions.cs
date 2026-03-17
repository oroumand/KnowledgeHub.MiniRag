using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeHub.MiniRag.Infras.AI.Shared.AI;

public class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    public string ApiKey { get; set; } = string.Empty;

    public string EmbeddingModel { get; set; } = "text-embedding-3-small";

    public string ChatModel { get; set; } = "gpt-4.1-mini";
}
