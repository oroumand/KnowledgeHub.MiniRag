using KnowledgeHub.MiniRag.Core.Applicaiton.Documents.Commands;
using KnowledgeHub.MiniRag.Core.Applicaiton.Shared.DependencyInjection;
using KnowledgeHub.MiniRag.Endpoints.Api.Documents.Endpoints;
using KnowledgeHub.MiniRag.Endpoints.Api.Search.Endpoints;
using KnowledgeHub.MiniRag.Infras.AI.Shared.DependencyInjection;
using KnowledgeHub.MiniRag.Infras.SqlServer.Shared.DependencyInjection;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAI(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/", () => Results.Ok(new
{
    Name = "KnowledgeHub.MiniRag API",
    Status = "Running"
}));

app.MapDocumentEndpoints("/documents");
app.MapSearchEndpoints("/search");
app.Run();
