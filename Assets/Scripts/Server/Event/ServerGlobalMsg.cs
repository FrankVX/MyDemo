using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class EventTypeAttribute : Attribute
{

}
[EventType]
public enum ServerGlobalMsg : short
{
    ServerStart,
    ServerStop,
    OnClientConnect,
    OnClienReady,
}

