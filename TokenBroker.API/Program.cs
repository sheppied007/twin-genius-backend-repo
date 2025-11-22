using TokenBroker.API.Endpoints;
using TokenBroker.Application;
using TokenBroker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Cache
builder.Services.AddMemoryCache();

//Dependency injection
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:8080", "https://app-digitwin-prod-fae0f8baergjhpfx.uksouth-01.azurewebsites.net") // React dev server origin
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

app.UseCors("AllowFrontend");

//Endpoints
app.MapTokenEndpoints();
app.MapHealthEndpoints();

app.Run();
