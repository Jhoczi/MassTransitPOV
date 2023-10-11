using MassTransit;
using MassTransitPov.Api.Publishers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory((context,cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
    
    x.AddConsumers(typeof(Program).Assembly);
});

builder.Services.AddHostedService<PingPublisher>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();