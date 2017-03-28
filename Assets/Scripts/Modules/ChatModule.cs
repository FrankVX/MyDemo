using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatModule : BaseGameModule
{
    protected override void Awake()
    {
        base.Awake();
    }
    [Server]
    private void SendChat(NetworkConnection conn, string text)
    {
        RpcShowChat(text);
    }

    [ClientRpc]
    public void RpcShowChat(string text)
    {
        Dispatch(ChatEvent.ShowChat, text);
    }
}
