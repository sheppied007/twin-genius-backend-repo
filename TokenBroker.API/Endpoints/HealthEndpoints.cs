using MediatR;

namespace TokenBroker.API.Endpoints;

public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", async () =>
        {
            return Results.Ok("Healthy");
        });
    }
}