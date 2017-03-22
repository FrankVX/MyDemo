﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class EventTypeAttribute : Attribute
{

}
[EventType]
public enum GameMsgType : short
{
    None = MsgType.Highest + 1,
    ServerStart,
    ServerStop,
    ShowChat,
    SendChat

}
