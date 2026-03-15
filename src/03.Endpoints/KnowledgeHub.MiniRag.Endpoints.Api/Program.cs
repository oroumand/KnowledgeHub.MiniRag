var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

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
