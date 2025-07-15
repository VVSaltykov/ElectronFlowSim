using System.Text.Json;
using Confluent.Kafka;
using ElectronFlowSim.AnalysisService.Services.ElectronFlow;

namespace ElectronFlowSim.AnalysisService.Services.Kafka;

public class KafkaConsumerService : IKafkaConsumerService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _topic;
    private readonly IConsumer<string, string> _consumer;

    private readonly IElectronFlowService _electronFlowService;

    public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger, IElectronFlowService electronFlowService)
    {
        _configuration = configuration;
        _logger = logger;
        _topic = configuration["Kafka:Topic"];

        var config = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();

        _electronFlowService = electronFlowService;
    }

    /// <summary>
    /// Слушание входящих сообщений
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StartConsumingAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_topic);
        _logger.LogInformation($"Subscribed to Kafka topic: {_topic}");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);

                    string innerJson = JsonSerializer.Deserialize<string>(result.Message.Value);

                    var message = JsonSerializer.Deserialize<KafkaMessage>(innerJson);

                    _logger.LogInformation($"Получено сообщение: Key = {result.Message.Key}, Value = {result.Message.Value}");

                    await _electronFlowService.RunExecutableAsync(message.FileContent, message.RequestId, message.ConnectionId);
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Ошибка при десериализации Kafka-сообщения.");
                }
                catch (ConsumeException consumeEx)
                {
                    _logger.LogError(consumeEx, "Ошибка при получении сообщения из Kafka.");
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Остановка потребителя Kafka...");
        }
        finally
        {
            _consumer.Close();
        }
    }
}
