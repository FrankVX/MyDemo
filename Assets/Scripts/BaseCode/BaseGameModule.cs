using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class BaseGameModule : GameNetBehaviour
{

    protected override void Awake()
    {
        base.Awake();
       
    }

    void RegsiterHandlers()
    {
        var methods = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        Handler handler = new Handler();
        handler.identity = netIdentity;
        handler.module = this;
        foreach (var m in methods)
        {
            if (m.IsDefined(typeof(ServerAttribute), true))
            {
                handler.AddMethod(m);
            }
        }
        NetMessageHandler.RegsiterHandler(handler);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        RegsiterHandlers();
    }

    public void Command(string name, params object[] args)
    {
        NetMessageHandler.SendMsg(Clien.connection, netId, name, args);
    }

    public void RPC(string name, NetworkConnection target, params object[] args)
    {
        NetMessageHandler.SendMsg(target, netId, name, args);
    }

    public void RPCAll(string name, params object[] args)
    {
        foreach (var net in NetworkServer.connections)
        {
            NetMessageHandler.SendMsg(net, netId, name, args);
        }
    }
}
