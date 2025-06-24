using System.Text.Json;
using ElectronFlowSim.API.Services.File;
using ElectronFlowSim.API.Services.Kafka;
using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.API.Services.ElectronFlow;

public class ElectronFlowService : IElectronFlowService
{
    private readonly IKafkaProducerService _producerService;
    private readonly ITxtFileService _txtFileService;

    public ElectronFlowService(IKafkaProducerService producerService, ITxtFileService txtFileService)
    {
        _producerService = producerService;
        _txtFileService = txtFileService;
    }
    public async Task DrawingFlow(InputDataDTO inputDataDTO, string requestId, string connectionId)
    {
        string fileContent = await _txtFileService.CreateInputFile(inputDataDTO);

        var message = new
        {
            Content = fileContent,
            RequestId = requestId,
            ConnectionId = connectionId
        };

        await _producerService.ProduceAsync("electron-flow-topic", JsonSerializer.Serialize(message));
    }
}