using LCS.Domain.Interfaces;

namespace LCS.Infra.Services;

public class EnvironmentService : IEnvironmentService
{
    public string EnvironmentName =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
}