using LCS.Application.Abstractions;
using Microsoft.Extensions.Hosting;

namespace LCS.Infra.Services;

public sealed class EnvironmentService : IEnvironmentService
{
    private readonly IHostEnvironment _hostEnvironment;

    public EnvironmentService(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public string EnvironmentName => _hostEnvironment.EnvironmentName;
}
