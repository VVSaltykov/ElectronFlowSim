using ElectronFlowSim.AnalysisService.Services.Kafka;

namespace ElectronFlowSim.AnalysisService.BackGroundServices;

public class KafkaBackgroundService : BackgroundService
{
    private readonly IKafkaConsumerService _consumerService;
    private readonly ILogger<KafkaBackgroundService> _logger;

    public KafkaBackgroundService(IKafkaConsumerService kafkaConsumerService, ILogger<KafkaBackgroundService> logger)
    {
        _consumerService = kafkaConsumerService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumerService.StartConsumingAsync(stoppingToken);
    }
}
