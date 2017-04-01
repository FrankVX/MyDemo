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
    private void ReceiveChat(NetworkConnection conn, string text)
    {
        RpcShowChat(text);
    }

    #endregion


    //====================================================================================================


    #region Client

    public void SendChat(string text)
    {
        Command("ReceiveChat", text);
    }


    [ClientRpc]
    public void RpcShowChat(string text)
    {
        Dispatch(ChatEvent.ShowChat, text);
    }
    #endregion
}
