using StackExchange.Redis;

ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:1923");

ISubscriber subscriber = redis.GetSubscriber();

await subscriber.SubscribeAsync("#mychannel", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();


