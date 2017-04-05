using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class ModuleMediator : GameNetBehaviour
{

    public override void OnStartServer()
    {
        base.OnStartServer();
        RegsitCommand();
    }

    void RegsitCommand()
    {
        Handler handler = new Handler();
        handler.identity = netIdentity;
        handler.obj = this;
        var ms = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        foreach (var m in ms)
        {
            if (m.IsDefined(typeof(ServerAttribute), true))
            {
                handler.AddMethod(m);
            }
        }
        NetMessageHandler.RegsiterHandler(handler);
    }

    protected void Command(string name, params object[] args)
    {
        if (NetworkManager.singleton && NetworkManager.singleton.client != null)
        {
            var conn = NetworkManager.singleton.client.connection;
            if (conn != null)
                NetMessageHandler.SendMessage(conn, netId, name, args);
        }
    }
}
