using MassTransit;
using MassTransitPov.Common.Messages;

var busControl = Bus.Factory.CreateUsingAmazonSqs(cfg =>
{
    cfg.Host("eu-central-1", h =>
    {
        h.AccessKey("XYZ");
        h.SecretKey("XYZ");
    });
});

await busControl.StartAsync();

var endpoint = await busControl.GetSendEndpoint(new Uri("queue:mtp-example"));

while (true)
{
    Console.WriteLine("Preparing message to be sent...");

    await endpoint.Send(new NewMovieMessage
    {
        Id = Guid.NewGuid(),
        TimeStamp = Faker.DateTimeFaker.DateTime(),
        Creator = Faker.NameFaker.Name(),
        Description = Faker.TextFaker.Sentence(),
        Title = Faker.CompanyFaker.Name(),
        YearOfCreation = Faker.DateTimeFaker.DateTime().Year
        
    });
    Console.WriteLine("Message sent! ");
    Thread.Sleep(3_000);
} 
await busControl.StopAsync(); // Stop the bus



