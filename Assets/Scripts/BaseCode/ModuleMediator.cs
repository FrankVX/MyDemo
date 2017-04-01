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
        handler.module = this;
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
        NetMessageHandler.SendCommand(netId, name, args);
    }

}
