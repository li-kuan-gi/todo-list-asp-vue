using Microsoft.EntityFrameworkCore;
using Server.Todo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

builder.Services.AddDbContext<TodoContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapGet("/", () => Results.File("index.html", "text/html"));

app.UseAuthorization();

app.MapControllers();

app.Run();
