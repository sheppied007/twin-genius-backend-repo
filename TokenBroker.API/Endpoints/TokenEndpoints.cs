using MediatR;
using TokenBroker.Application.Token.Queries;

namespace TokenBroker.API.Endpoints;

public static class TokenEndpoints
{
    public static void MapTokenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/token/adt", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAdtTokenQuery());
            return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Message);
        });

        app.MapGet("/api/token/storage", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetStorageTokenQuery());
            return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Message);
        });
    }
}