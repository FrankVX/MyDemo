using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserDataContainer : GameNetBehaviour
{
    public NetworkConnection Owner { get; set; }
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        var types = GameManager.GetSubTypes<UserDataBase>();
        foreach (var type in types)
        {
            gameObject.AddComponent(type);
        }
    }

    public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize)
    {
        observers.Clear();
        observers.Add(Owner);
        return true;
    }

    public override bool OnCheckObserver(NetworkConnection conn)
    {
        if (this.Owner == conn)
            return true;
        return false;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

    }

}
