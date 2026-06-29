using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Seed;
using Dsw2026Ej15.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Dsw2026Ej15DbContext>();
builder.Services.AddScoped<IPersistence, PersistenceEf>();
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

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<Dsw2026Ej15DbContext>();
    await DbSeeder.Seed(context);
}
app.UseExceptionHandler();
app.MapHealthChecks("/health-check");
app.UseAuthorization();

app.MapControllers();

app.Run();
