using System.ComponentModel.DataAnnotations;

public sealed record AskChatRequest(
    [property: Required]
    [property: MinLength(3)]
    string Question,

    [property: Range(1, 10)]
    int TopK = 5
);