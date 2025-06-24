using ElectronFlowSim.DTO.AnalysisService;

namespace ElectronFlowSim.API.Services.File;

public interface ITxtFileService
{
    Task<string> CreateInputFile(InputDataDTO inputDataDTO);
    Task GetOutputFile(OutputDataDTO outputDataDTO);
}