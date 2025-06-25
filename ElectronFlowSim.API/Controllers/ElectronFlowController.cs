using System.ComponentModel.DataAnnotations;
using Electronflow;
using ElectronFlowSim.API.Services.ElectronFlow;
using ElectronFlowSim.DTO.AnalysisService;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("get-magnetic-fields")]
    public async Task<IActionResult> GetMagneticFields(
        [FromQuery] double startPoint,
        [FromQuery] double endPoint,
        [FromQuery, Range(0.1, 5)] double step)
    {
        var grpcRequest = new DataRequest { StartPoint = startPoint, EndPoint = endPoint, Step = step };
        var grpcResponse = await _magneticFieldsGrpcClient.GetMagneticFieldsAsync(grpcRequest);

        return Ok(grpcResponse);
    }
} 
