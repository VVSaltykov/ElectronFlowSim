namespace ElectronFlowSim.DTO.AnalysisService;

/// <summary>
/// DTO данных магнитных полей
/// </summary>
public class BMDataDTO
{
    /// <summary>
    /// массив точек
    /// </summary>
    public List<double> z {  get; set; }

    /// <summary>
    /// массив значений магнитного поля
    /// </summary>
    public List<double> bm { get; set; }

    /// <summary>
    /// максимальное значение магнитного поля
    /// </summary>
    public double bnorm { get; set; }
}