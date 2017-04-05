using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using System.Reflection;
using System.Diagnostics;

public class RemoteAttribute : Attribute
{

}

public class Handler
{
    public Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();
    public NetworkIdentity identity;
    public object obj;

    public void AddMethod(MethodInfo info)
    {
        methodInfos[info.Name] = info;
    }
}

public class NetMessageHandler
{
    static Dictionary<NetworkInstanceId, Handler> handlers = new Dictionary<NetworkInstanceId, Handler>();
    static Dictionary<string, Handler> singleClassHandlers = new Dictionary<string, Handler>();
    public static void RegsiterHandler(Handler handler)
    {
        if (handler.identity != null)
        {
            handlers[handler.identity.netId] = handler;
        }
        else
        {
            singleClassHandlers[handler.obj.GetType().FullName] = handler;
        }
    }

    private static void OnReceiveMessage(NetworkMessage msg)
    {
        var sender = msg.conn;
        Handler handler = null;
        if (msg.reader.ReadBoolean())
        {
            var id = msg.reader.ReadNetworkId();
            handlers.TryGetValue(id, out handler);
        }
        else
        {
            var className = msg.reader.ReadString();
            singleClassHandlers.TryGetValue(className, out handler);
        }
        if (handler == null || handler.methodInfos == null) return;
        var name = msg.reader.ReadString();
        MethodInfo method;
        if (!handler.methodInfos.TryGetValue(name, out method)) return;
        var ps = method.GetParameters();
        object[] args = msg.reader.ReadData(sender, method);
        method.Invoke(handler.obj, args);
    }


    public static void SendMessage(NetworkConnection target, NetworkInstanceId id, string name, object[] args)
    {
        NetworkWriter writer = new NetworkWriter();
        writer.StartMessage(99);
        writer.Write(true);
        writer.Write(id);
        writer.Write(name);
        writer.WriteData(args);
        writer.FinishMessage();
        target.SendWriter(writer, 1);
    }

    public static void SendMessage(NetworkConnection target, object obj, string name, object[] args)
    {
        NetworkWriter writer = new NetworkWriter();
        writer.StartMessage(99);
        writer.Write(false);
        writer.Write(obj.GetType().FullName);
        writer.Write(name);
        writer.WriteData(args);
        writer.FinishMessage();
        target.SendWriter(writer, 1);
    }

    public static void ServerStart()
    {
        NetworkServer.RegisterHandler(99, OnReceiveMessage);
    }

    public static void ClientStart(NetworkConnection server)
    {
        server.RegisterHandler(99, OnReceiveMessage);
    }
}
