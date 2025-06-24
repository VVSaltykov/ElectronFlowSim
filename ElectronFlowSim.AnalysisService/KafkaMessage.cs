using System.Text.Json.Serialization;

namespace ElectronFlowSim.AnalysisService;

public class KafkaMessage
{
    [JsonPropertyName("Content")]
    public string FileContent { get; set; }
    public string RequestId { get; set; }
    public string ConnectionId { get; set; }

}