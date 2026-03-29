using LCS.Application.Features.Ping.Queries;
using MediatR;

namespace LCS.Api.Endpoints;

public static class PingEndpoints
{
    public static IEndpointRouteBuilder MapPingEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1")
            .WithTags("Health");

        group.MapGet("/ping", HandlePing)
            .WithName("Ping")
            .WithSummary("Check API health")
            .Produces<PingResponse>()
            .AllowAnonymous();

        return app;
    }

    private static async Task<IResult> HandlePing(
        IMediator mediator,
        CancellationToken ct)
    {
        var result = await mediator.Send(new PingQuery(), ct);
        return Results.Ok(result);
    }
}