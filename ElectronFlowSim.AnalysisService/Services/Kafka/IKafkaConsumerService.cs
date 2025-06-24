namespace ElectronFlowSim.AnalysisService.Services.Kafka;

public interface IKafkaConsumerService
{
    Task StartConsumingAsync(CancellationToken cancellationToken);
}