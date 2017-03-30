using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class EventTypeAttribute : Attribute { }


[EventType]
public enum ServerGlobalMsg
{
    OnStartServer,
    ServerStarted,
    ServerStoped,
    
}

[EventType]
public enum ClientGlobalMsg
{
    OnStartClient,
    OnClientConnect,
    OnClienReady,
}

