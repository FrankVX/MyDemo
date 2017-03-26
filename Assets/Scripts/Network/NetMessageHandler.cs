using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using System.Reflection;
using System.Diagnostics;


public class Handler
{
    public Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();
    public NetworkIdentity identity;
    public BaseGameModule module;

    public void AddMethod(MethodInfo info)
    {
        methodInfos[info.Name] = info;
    }
}

public class NetMessageHandler
{
    static Dictionary<NetworkInstanceId, Handler> handlers = new Dictionary<NetworkInstanceId, Handler>();

    public static void RegsiterHandler(Handler handler)
    {
        handlers[handler.identity.netId] = handler;
    }

    private static void OnReceiveMessage(NetworkMessage msg)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        var sender = msg.conn;
        var id = msg.reader.ReadNetworkId();
        Handler handler;
        if (!handlers.TryGetValue(id, out handler)) return;
        if (handler.methodInfos == null) return;
        var name = msg.reader.ReadString();
        MethodInfo method;
        if (!handler.methodInfos.TryGetValue(name, out method)) return;

        var ps = method.GetParameters();
        object[] args = new object[ps.Length];
        for (int i = 0; i < ps.Length; i++)
        {
            var pt = ps[i].ParameterType;
            if (pt.Equals(typeof(int)))
            {
                args[i] = msg.reader.ReadInt32();
            }
            else if (pt.Equals(typeof(uint)))
            {
                args[i] = msg.reader.ReadUInt32();
            }
            else if (pt.Equals(typeof(float)))
            {
                args[i] = msg.reader.ReadSingle();
            }
            else if (pt.Equals(typeof(string)))
            {
                args[i] = msg.reader.ReadString();
            }
            else if (pt.Equals(typeof(long)))
            {
                args[i] = msg.reader.ReadInt64();
            }
            else if (pt.Equals(typeof(ulong)))
            {
                args[i] = msg.reader.ReadUInt64();
            }
            else if (pt.Equals(typeof(byte)))
            {
                args[i] = msg.reader.ReadByte();
            }
            else if (pt.Equals(typeof(char)))
            {
                args[i] = msg.reader.ReadChar();
            }
            else if (pt.Equals(typeof(bool)))
            {
                args[i] = msg.reader.ReadBoolean();
            }
            else if (pt.Equals(typeof(Vector3)))
            {
                args[i] = msg.reader.ReadVector3();
            }
            else if (pt.Equals(typeof(Vector2)))
            {
                args[i] = msg.reader.ReadVector2();
            }
            else if (pt.Equals(typeof(Transform)))
            {
                args[i] = msg.reader.ReadTransform();
            }
            else if (pt.Equals(typeof(Quaternion)))
            {
                args[i] = msg.reader.ReadQuaternion();
            }
            else if (pt.Equals(typeof(NetworkInstanceId)))
            {
                args[i] = msg.reader.ReadNetworkId();
            }
            else if (pt.Equals(typeof(NetworkIdentity)))
            {
                args[i] = msg.reader.ReadNetworkIdentity();
            }
            else if (pt.Equals(typeof(NetworkHash128)))
            {
                args[i] = msg.reader.ReadNetworkHash128();
            }
            else if (pt.Equals(typeof(GameObject)))
            {
                args[i] = msg.reader.ReadGameObject();
            }
            else if (pt.IsSubclassOf(typeof(MessageBase)))
            {
                var m = Activator.CreateInstance(pt) as MessageBase;
                m.Deserialize(msg.reader);
                args[i] = m;
            }
        }
        method.Invoke(handler.module, args);
        UnityEngine.Debug.Log("receive" + watch.ElapsedMilliseconds);
        watch.Reset();
    }


    public static void SendMsg(NetworkConnection connection, NetworkInstanceId id, string name, params object[] args)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        NetworkWriter writer = new NetworkWriter();
        writer.StartMessage(99);
        writer.Write(id);
        writer.Write(name);
        foreach (var arg in args)
        {
            if (arg is int)
            {
                writer.Write((int)arg);
            }
            else if (arg is uint)
            {
                writer.Write((uint)arg);
            }
            else if (arg is string)
            {
                writer.Write((string)arg);
            }
            else if (arg is float)
            {
                writer.Write((float)arg);
            }
            else if (arg is long)
            {
                writer.Write((long)arg);
            }
            else if (arg is ulong)
            {
                writer.Write((ulong)arg);
            }
            else if (arg is byte)
            {
                writer.Write((byte)arg);
            }
            else if (arg is char)
            {
                writer.Write((char)arg);
            }
            else if (arg is bool)
            {
                writer.Write((bool)arg);
            }
            else if (arg is Vector3)
            {
                writer.Write((Vector3)arg);
            }
            else if (arg is MessageBase)
            {
                (arg as MessageBase).Serialize(writer);
            }
        }
        connection.SendWriter(writer, 1);
        UnityEngine.Debug.Log("send" + watch.ElapsedMilliseconds);
        watch.Reset();
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
