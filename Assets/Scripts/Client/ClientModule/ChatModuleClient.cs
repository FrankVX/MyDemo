using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public enum ChatEvent
{
    SendChat,
    ShowChat,
}
class ChatModuleClient : ClientModule
{
    protected override void Awake()
    {
        base.Awake();
        AddListener(ChatEvent.SendChat, "SendChat");
    }

    void SendChat(string text)
    {
        Command("SendChat", text);
    }
}

