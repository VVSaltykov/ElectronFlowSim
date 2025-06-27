using Electronflow;
using ElectronFlowSim.API.Services.ElectronFlow;
using ElectronFlowSim.DTO.AnalysisService;
using ElectronFlowSim.DTO.API;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElectronFlowSim.API.Controllers;

[ApiController]
[Route("api/electronflow/")]
public class ElectronFlowController : ControllerBase
{
    private readonly IElectronFlowService _electronFlowService;
    private readonly ElectronFlow.ElectronFlowClient _electronFlowGrpcClient;
    private readonly MagneticFields.MagneticFieldsClient _magneticFieldsGrpcClient;

    public ElectronFlowController(IElectronFlowService electronFlowService, ElectronFlow.ElectronFlowClient electronFlowGrpcClient,
        MagneticFields.MagneticFieldsClient magneticFieldsGrpcClient)
    {
        _electronFlowService = electronFlowService;
        _electronFlowGrpcClient = electronFlowGrpcClient;
        _magneticFieldsGrpcClient = magneticFieldsGrpcClient;
    }

    [HttpPost("drawing-flow")]
    public async Task<IActionResult> DrawingFlow([FromBody] InputDataDTO inputDataDTO,
        [FromHeader(Name = "X-Connection-ID")] string? connectionId = null)
    {
        var requestId = Guid.NewGuid().ToString();

        connectionId ??= HttpContext.Request.Headers["X-SignalR-Connection-ID"];

        await _electronFlowService.DrawingFlow(inputDataDTO, requestId, connectionId);

        return Ok(new
        {
            RequestId = requestId,
            Message = "Processing started"
        });
    }

    [HttpGet("get-drawing-result")]
    public async Task<IActionResult> GetDrawingResult([FromQuery] string folderId)
    {
        var grpcRequest = new FileRequest { Folderid = folderId };
        var grpcResponse = await _electronFlowGrpcClient.ParseFileAsync(grpcRequest);

        return Ok(grpcResponse);
    }

    [HttpPost("get-magnetic-fields")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> GetMagneticFields([FromForm] MagneticFieldsDTO magneticFieldsDTO)
    {
        if (magneticFieldsDTO.File == null || magneticFieldsDTO.File.Length == 0)
            return BadRequest("Файл не загружен.");

        using var stream = magneticFieldsDTO.File.OpenReadStream();

        var grpcRequest = new MagneticFieldsFileRequest
        {
            StartPoint = magneticFieldsDTO.StartPoint,
            EndPoint = magneticFieldsDTO.EndPoint,
            Step = magneticFieldsDTO.Step,
            FileContent = await ByteString.FromStreamAsync(stream)
        };

        var grpcResponse = await _magneticFieldsGrpcClient.GetMagneticFieldsFromFileAsync(grpcRequest);

        return Ok(grpcResponse);
    }
}
