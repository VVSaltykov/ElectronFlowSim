using System.Text.Json;
using Confluent.Kafka;

namespace ElectronFlowSim.API.Services.Kafka;

public class KafkaProducerService : IKafkaProducerService, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducerService> _logger;

    public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
    {
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync<T>(string topic, T message)
    {
        await ProduceAsync(topic, Guid.NewGuid().ToString(), message);
    }

    public async Task ProduceAsync<T>(string topic, string key, T message)
    {
        var value = JsonSerializer.Serialize(message);
        await ProduceRawAsync(topic, key, value);
    }

    public async Task ProduceRawAsync(string topic, string key, string value)
    {
        try
        {
            var result = await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = key,
                Value = value
            });

            _logger.LogInformation($"Сообщение отправлено в Kafka: {result.TopicPartitionOffset}");
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError($"Ошибка отправки в Kafka: {ex.Error.Reason}");
            throw;
        }
    }

    public async Task ProduceAsync<T>(string topic, string key, T message, Action<DeliveryReport<string, string>> deliveryHandler)
    {
        var value = JsonSerializer.Serialize(message);
        _producer.Produce(topic, new Message<string, string>
        {
            Key = key,
            Value = value
        }, deliveryHandler);
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}
