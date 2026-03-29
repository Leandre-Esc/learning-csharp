using LCS.Api.Endpoints;
using LCS.Application;
using LCS.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddInfraServices();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.MapPingEndpoints();

app.Run();