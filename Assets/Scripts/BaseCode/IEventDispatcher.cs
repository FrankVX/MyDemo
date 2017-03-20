using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
public interface IEventData
{

}
public interface IEventDispatcher
{
    void AddListener(object type, object handler);


}


