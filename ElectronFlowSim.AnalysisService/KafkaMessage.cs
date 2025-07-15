using System.Text.Json.Serialization;

namespace ElectronFlowSim.AnalysisService;

/// <summary>
/// Сообщение, которое приходит в топик
/// </summary>
public class KafkaMessage
{
    /// <summary>
    /// Файл с входными данными
    /// </summary>
    [JsonPropertyName("Content")]
    public string FileContent { get; set; }
    /// <summary>
    /// Id запроса
    /// </summary>
    public string RequestId { get; set; }
    /// <summary>
    /// Id пользователя
    /// </summary>
    public string ConnectionId { get; set; }
}