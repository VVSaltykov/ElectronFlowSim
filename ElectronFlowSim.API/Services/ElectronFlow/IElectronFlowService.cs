using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.API.Services.ElectronFlow;

public interface IElectronFlowService
{
    Task DrawingFlow(InputDataDTO inputDataDTO, string requestId, string connectionId);
}