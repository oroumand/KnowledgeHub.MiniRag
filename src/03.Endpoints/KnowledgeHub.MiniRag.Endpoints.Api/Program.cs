using KnowledgeHub.MiniRag.Infras.AI.Shared.DependencyInjection;
using KnowledgeHub.MiniRag.Infras.SqlServer.Shared.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAI(builder.Configuration);

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

app.MapControllers();

app.Run();
