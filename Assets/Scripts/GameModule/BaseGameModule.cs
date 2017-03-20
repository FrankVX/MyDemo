using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class BaseGameModule : GameNetBehaviour
{

    protected override void Awake()
    {
        base.Awake();
        var methods = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        Handler handler = new Handler();
        handler.identity = netIdentity;
        handler.module = this;
        foreach (var m in methods)
        {
            if (m.IsDefined(typeof(MsgAttribute), true))
            {
                handler.AddMethod(m);
            }
        }
        NetMessageHandler.RegsiterHandler(handler);
    }

    private void Command(string name, params object[] args)
    {
        NetMessageHandler.SendMsg(connectionToServer, netId, name, args);
    }

    private void RPC(string name, NetworkConnection target, params object[] args)
    {
        NetMessageHandler.SendMsg(target, netId, name, args);
    }
}
