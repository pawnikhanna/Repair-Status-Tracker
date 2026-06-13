using RepairTracker.Api.Repositories;
using RepairTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Core Architecture Registrations (Dependency Injection)
builder.Services.AddSingleton<IRepairJobRepository, InMemoryRepairJobRepository>();
builder.Services.AddScoped<IRepairJobService, RepairJobService>();

// Configure open CORS policy for regional UI communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Default Vite dev port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();