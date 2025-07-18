// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/magneticfields.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Electronflow {
  public static partial class MagneticFields
  {
    static readonly string __ServiceName = "electronflow.MagneticFields";

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
    static readonly grpc::Marshaller<global::Electronflow.MagneticFieldsFileRequest> __Marshaller_electronflow_MagneticFieldsFileRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Electronflow.MagneticFieldsFileRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Electronflow.MagneticFieldsOutputResponse> __Marshaller_electronflow_MagneticFieldsOutputResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Electronflow.MagneticFieldsOutputResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Electronflow.NZRUTableDataFileRequest> __Marshaller_electronflow_NZRUTableDataFileRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Electronflow.NZRUTableDataFileRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Electronflow.NZRUTableDataOutputResponse> __Marshaller_electronflow_NZRUTableDataOutputResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Electronflow.NZRUTableDataOutputResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Electronflow.NLTableDataFileRequest> __Marshaller_electronflow_NLTableDataFileRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Electronflow.NLTableDataFileRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Electronflow.NLTableDataOutputResponse> __Marshaller_electronflow_NLTableDataOutputResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Electronflow.NLTableDataOutputResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Electronflow.MagneticFieldsFileRequest, global::Electronflow.MagneticFieldsOutputResponse> __Method_GetMagneticFieldsFromFile = new grpc::Method<global::Electronflow.MagneticFieldsFileRequest, global::Electronflow.MagneticFieldsOutputResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetMagneticFieldsFromFile",
        __Marshaller_electronflow_MagneticFieldsFileRequest,
        __Marshaller_electronflow_MagneticFieldsOutputResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Electronflow.NZRUTableDataFileRequest, global::Electronflow.NZRUTableDataOutputResponse> __Method_GetNZRUTableDataFromFile = new grpc::Method<global::Electronflow.NZRUTableDataFileRequest, global::Electronflow.NZRUTableDataOutputResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetNZRUTableDataFromFile",
        __Marshaller_electronflow_NZRUTableDataFileRequest,
        __Marshaller_electronflow_NZRUTableDataOutputResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Electronflow.NLTableDataFileRequest, global::Electronflow.NLTableDataOutputResponse> __Method_GetNLTableDataFromFile = new grpc::Method<global::Electronflow.NLTableDataFileRequest, global::Electronflow.NLTableDataOutputResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetNLTableDataFromFile",
        __Marshaller_electronflow_NLTableDataFileRequest,
        __Marshaller_electronflow_NLTableDataOutputResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Electronflow.MagneticfieldsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for MagneticFields</summary>
    public partial class MagneticFieldsClient : grpc::ClientBase<MagneticFieldsClient>
    {
      /// <summary>Creates a new client for MagneticFields</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public MagneticFieldsClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for MagneticFields that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public MagneticFieldsClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected MagneticFieldsClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected MagneticFieldsClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Electronflow.MagneticFieldsOutputResponse GetMagneticFieldsFromFile(global::Electronflow.MagneticFieldsFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMagneticFieldsFromFile(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Electronflow.MagneticFieldsOutputResponse GetMagneticFieldsFromFile(global::Electronflow.MagneticFieldsFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetMagneticFieldsFromFile, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Electronflow.MagneticFieldsOutputResponse> GetMagneticFieldsFromFileAsync(global::Electronflow.MagneticFieldsFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMagneticFieldsFromFileAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Electronflow.MagneticFieldsOutputResponse> GetMagneticFieldsFromFileAsync(global::Electronflow.MagneticFieldsFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetMagneticFieldsFromFile, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Electronflow.NZRUTableDataOutputResponse GetNZRUTableDataFromFile(global::Electronflow.NZRUTableDataFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetNZRUTableDataFromFile(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Electronflow.NZRUTableDataOutputResponse GetNZRUTableDataFromFile(global::Electronflow.NZRUTableDataFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetNZRUTableDataFromFile, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Electronflow.NZRUTableDataOutputResponse> GetNZRUTableDataFromFileAsync(global::Electronflow.NZRUTableDataFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetNZRUTableDataFromFileAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Electronflow.NZRUTableDataOutputResponse> GetNZRUTableDataFromFileAsync(global::Electronflow.NZRUTableDataFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetNZRUTableDataFromFile, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Electronflow.NLTableDataOutputResponse GetNLTableDataFromFile(global::Electronflow.NLTableDataFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetNLTableDataFromFile(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Electronflow.NLTableDataOutputResponse GetNLTableDataFromFile(global::Electronflow.NLTableDataFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetNLTableDataFromFile, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Electronflow.NLTableDataOutputResponse> GetNLTableDataFromFileAsync(global::Electronflow.NLTableDataFileRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetNLTableDataFromFileAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Electronflow.NLTableDataOutputResponse> GetNLTableDataFromFileAsync(global::Electronflow.NLTableDataFileRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetNLTableDataFromFile, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override MagneticFieldsClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new MagneticFieldsClient(configuration);
      }
    }

  }
}
#endregion
