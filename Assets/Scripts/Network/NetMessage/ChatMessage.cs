using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public enum ChatType
{
    All,
    Target
}
public class ChatMessage : MessageBase
{
    public string message;
    public ChatType type;
    public NetworkInstanceId netID;

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(message);
        writer.Write((int)type);
        if (type == ChatType.Target)
        {
            writer.Write(netID);
        }
    }

    public override void Deserialize(NetworkReader reader)
    {
        base.Deserialize(reader);
        message = reader.ReadString();
        type = (ChatType)reader.ReadInt32();
        if (type == ChatType.Target)
            netID = reader.ReadNetworkId();
    }
}
