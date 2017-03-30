using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatMediator : ModuleMediator
{
    protected override void Awake()
    {
        base.Awake();
        print("ChatModule Awake");
    }

    #region Server

    [Server]
    private void SendChat(NetworkConnection conn, string text)
    {
        RpcShowChat(text);
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        base.OnStartClient();
        Dispatch(ChatEvent.ShowChat, string.Format("OnStartClient , id = {0},assetid = {1}", netId, netIdentity.assetId));
    }


    [ClientRpc]
    public void RpcShowChat(string text)
    {
        Dispatch(ChatEvent.ShowChat, text);
    }
    #endregion
}
