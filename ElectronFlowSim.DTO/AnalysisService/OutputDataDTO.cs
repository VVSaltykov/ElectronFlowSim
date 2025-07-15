using ElectronFlowSim.DTO.AnalysisService.OutputData;

namespace ElectronFlowSim.DTO.AnalysisService;

/// <summary>
/// DTO для получения выходных данных
/// </summary>
public class OutputDataDTO
{
    public List<TrajectoryPoint> TrajectoryPoints { get; set; } = new();
    public FinalParameters FinalParams { get; set; }
    public List<UekvipEntry> UekvipList { get; set; } = new();
    public List<MicrowaveData> MicrowavePoints { get; set; } = new();
    public CathodeCurrentDensity CathodeDensity { get; set; }
    public BMData BmData { get; set; }
    public CalculationParameters CalcParams { get; set; }
    public FinalResult Result { get; set; }
    public List<EkvData> EkvList { get; set; }
    public List<EkvPointData> EkvPointList { get; set; }
}
