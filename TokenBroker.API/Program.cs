using TokenBroker.API.Endpoints;
using TokenBroker.Application;
using TokenBroker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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
            .WithOrigins("http://localhost:8080") // React dev server origin
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

app.Run();
