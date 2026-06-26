using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=Dsw2026Ej15;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True";
// Add services to the container.
builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IPersistence, PersistenceEF>();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.MapHealthChecks("/health-check");
app.UseAuthorization();

app.MapControllers();

app.Run();
