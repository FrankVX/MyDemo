using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System;

public class ChatModule : BaseGameModule
{

    void OnReceiveMsg(NetworkMessage msg)
    {
        RpcSendTextAll(string.Concat(
            msg.conn.connectionId,
            "  :    ",
            msg.ReadMessage<StringMessage>().value)
            );
    }

    void SendText(string text)
    {
        Clien.Send((short)88, new StringMessage(text));
    }

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
        AddListener(GameMsgType.SendChat, "SendText");
        NetworkServer.RegisterHandler(88, OnReceiveMsg);
        gameObject.SetActive(true);
        CreatInstance("UI/ChatUI");
    }

}
