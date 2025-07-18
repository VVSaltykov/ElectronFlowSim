// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/dbcommunication.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ElectronFlowSim.AnalysisService.GRPC.Protos {
  /// <summary>
  ///������� ��� ������ � ��
  /// </summary>
  public static partial class DBCommunication
  {
    static readonly string __ServiceName = "DBCommunication";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO> __Marshaller_InputDataDTO = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse> __Marshaller_EmptyResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest> __Marshaller_EmptyRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames> __Marshaller_SaveNames = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest> __Marshaller_GetSaveRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData> __Marshaller_SaveData = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO, global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse> __Method_CreateSave = new grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO, global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateSave",
        __Marshaller_InputDataDTO,
        __Marshaller_EmptyResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest, global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO> __Method_GetLastSave = new grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest, global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetLastSave",
        __Marshaller_EmptyRequest,
        __Marshaller_InputDataDTO);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest, global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames> __Method_GetSaveNames = new grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest, global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSaveNames",
        __Marshaller_EmptyRequest,
        __Marshaller_SaveNames);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest, global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData> __Method_GetSave = new grpc::Method<global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest, global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSave",
        __Marshaller_GetSaveRequest,
        __Marshaller_SaveData);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ElectronFlowSim.AnalysisService.GRPC.Protos.DbcommunicationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for DBCommunication</summary>
    public partial class DBCommunicationClient : grpc::ClientBase<DBCommunicationClient>
    {
      /// <summary>Creates a new client for DBCommunication</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DBCommunicationClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for DBCommunication that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public DBCommunicationClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DBCommunicationClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected DBCommunicationClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse CreateSave(global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSave(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse CreateSave(global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateSave, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse> CreateSaveAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSaveAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyResponse> CreateSaveAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateSave, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO GetLastSave(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetLastSave(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO GetLastSave(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetLastSave, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO> GetLastSaveAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetLastSaveAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.InputDataDTO> GetLastSaveAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetLastSave, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames GetSaveNames(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSaveNames(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames GetSaveNames(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSaveNames, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames> GetSaveNamesAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSaveNamesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveNames> GetSaveNamesAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.EmptyRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSaveNames, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData GetSave(global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSave(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData GetSave(global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSave, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData> GetSaveAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSaveAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ElectronFlowSim.AnalysisService.GRPC.Protos.SaveData> GetSaveAsync(global::ElectronFlowSim.AnalysisService.GRPC.Protos.GetSaveRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSave, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override DBCommunicationClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DBCommunicationClient(configuration);
      }
    }

  }
}
#endregion
