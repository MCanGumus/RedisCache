using StackExchange.Redis;

ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:1923");

ISubscriber subscriber = redis.GetSubscriber();

while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine();

    await subscriber.PublishAsync("#mychannel", message);
}




