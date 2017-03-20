using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class NetworkModuleBase : GameBehaviour
{
    

    public static void RegisterHandler(NetworkMsgType type, NetworkMessageDelegate handler)
    {
        NetworkServer.RegisterHandler((short)type, handler);
    }

    public static void Send(NetworkMsgType type, MessageBase msg)
    {
        Clien.Send((short)type, msg);
    }

    protected virtual void Init()
    {

    }
}
