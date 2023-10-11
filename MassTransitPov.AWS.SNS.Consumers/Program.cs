using MassTransit;
using MassTransitPov.AWS.SNS.Consumers;
using MassTransitPov.AWS.SNS.Consumers.Configuration;
using MassTransitPov.AWS.SNS.Consumers.Consumers;
using MassTransitPov.AWS.SNS.Consumers.Faults;
using MassTransitPov.AWS.SNS.Consumers.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ServiceConfiguration>();
builder.Services.AddMongoDb();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DbFaultConsumer>();
    x.UsingAmazonSqs((context, cfg) =>
    {
        var awsSettings = builder.Configuration.GetRequiredSection("AWS");
        
        cfg.Host(awsSettings.GetSection("Region").Value, h =>
        {
            h.AccessKey(awsSettings.GetRequiredSection("AccessKey").Value);
            h.SecretKey(awsSettings.GetRequiredSection("SecretKey").Value);
        });
        
        cfg.UseMessageRetry(r => r.Immediate(5));
        // cfg.UseDelayedRedelivery(r => 
        //     r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
        // cfg.UseMessageRetry(r =>
        // {
        //     r.Immediate(3);
        //     r.Ignore(typeof(InvalidOperationException));
        //     r.Ignore<ArgumentNullException>(t => t.ParamName == "something");
        // });
        
        cfg.ReceiveEndpoint(awsSettings.GetRequiredSection("QueueName").Value, endpoint =>
        {
            //endpoint.ConfigureConsumeTopology = false;
            endpoint.Consumer<NotificationConsumer>();
            endpoint.Consumer<BadThingsHappenConsumer>(c => {
                 //c.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));
                 // c.UseMessageRetry(re =>
                 // {
                 //     re.Immediate(2);
                 // }); 
            });
            //endpoint.Consumer<DbFaultConsumer>(context);
            endpoint.Consumer<DbFaultConsumer>(context);
        
            endpoint.Subscribe(awsSettings.GetRequiredSection("TopicName").Value, ev =>
            {
                ev.TopicAttributes["DisplayName"] = "New tickets topic";
                ev.TopicTags.Add("environment", "development");
            });
        
            //endpoint.Subscribe<NewReservationMessage>();
        });
    });
});

// var busControl = Bus.Factory.CreateUsingAmazonSqs(cfg =>
// {
//     cfg.Host("eu-central-1", h =>
//     {
//         h.AccessKey("XD");
//         h.SecretKey("XDD");
//     });
//
//     cfg.ReceiveEndpoint("sns-queue-example", endpoint =>
//     {
//         endpoint.ConfigureConsumeTopology = false;
//
//         endpoint.Consumer<NotificationConsumer>();
//         endpoint.Consumer<DbConsumer>();
//         
//         endpoint.Subscribe("event-topic-xd", ev =>
//         {
//             ev.TopicAttributes["DisplayName"] = "Public Event Topic";
//             ev.TopicTags.Add("environment", "development");
//         });
//         
//         //endpoint.Subscribe<NewReservationMessage>();
//     });
// });
// await busControl.StartAsync(); // Start the bus
// Console.WriteLine("Press any key to exit");
// await Task.Run(() => Console.ReadKey());
// await busControl.StopAsync(); // Stop the bus

builder.Services.AddSingleton<IReservationRepository, ReservationRepository>();
builder.Services.AddSingleton<IErrorRepository, ErrorRepository>();

var app = builder.Build();
app.Run();
