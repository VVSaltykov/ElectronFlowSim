namespace ElectronFlowSim.AnalysisService.Services.ElectronFlow;

public interface IElectronFlowService
{
    Task RunExecutableAsync(string inputFileContent, string requestId, string connectionId, CancellationToken cancellationToken = default);
}