using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System;

public class ChatModule : BaseGameModule
{


    [ClientRpc]
    public void RpcSendTextAll(string text)
    {
        Dispatch(GameMsgType.ShowChat, text);
    }
    [TargetRpc]
    public void TargetSentTargetText(NetworkConnection connect, string text)
    {
        print(text + "    [target]");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //AddListener(GameMsgType.SendChat, "SendText");
        GetSignal<SendChatText>().handler += SendChatText;
        gameObject.SetActive(true);
        CreatInstance("UI/ChatUI");
    }

    private void SendChatText(string obj)
    {
        Command("OnClientSendChatText", obj);
    }
    [Msg]
    void OnClientSendChatText(string text)
    {
        RpcSendTextAll(text);
    }
}
