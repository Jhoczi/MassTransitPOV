using MassTransit;
using MassTransitPov.Common.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingAmazonSqs((context, cfg) =>
    {
        var awsSettings = builder.Configuration.GetRequiredSection("AWS");
        cfg.Host(awsSettings.GetSection("Region").Value, h =>
        {
            h.AccessKey(awsSettings.GetRequiredSection("AccessKey").Value);
            h.SecretKey(awsSettings.GetRequiredSection("SecretKey").Value);
        });

        cfg.Message<NewReservationMessage>(e =>
        {
            e.SetEntityName(awsSettings.GetRequiredSection("TopicName").Value);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();