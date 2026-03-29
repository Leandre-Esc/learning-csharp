using LCS.Domain.Interfaces;
using MediatR;

namespace LCS.Application.Features.Ping.Queries;

public record PingQuery : IRequest<PingResponse>;

public record PingResponse(string Message, DateTimeOffset ServerTime, string Environment);

public class PingQueryHandler : IRequestHandler<PingQuery, PingResponse>
{
    private readonly IEnvironmentService _env;

    public PingQueryHandler(IEnvironmentService env)
    {
        _env = env;
    }

    public Task<PingResponse> Handle(PingQuery request, CancellationToken ct)
    {
        var response = new PingResponse(
            "pong",
            DateTimeOffset.UtcNow,
            _env.EnvironmentName
        );

        return Task.FromResult(response);
    }
}