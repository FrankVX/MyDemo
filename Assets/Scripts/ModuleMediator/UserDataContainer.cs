using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserDataContainer : GameNetBehaviour
{
    public NetworkConnection Owner { get; set; }

    Dictionary<Type, UserDataBase> datas = new Dictionary<Type, UserDataBase>();
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
            datas[type] = gameObject.AddComponent(type) as UserDataBase;
        }
    }

    public T GetDataComponent<T>() where T : UserDataBase
    {
        var type = typeof(T);
        UserDataBase data;
        if (datas.TryGetValue(type, out data))
        {
            return data as T;
        }
        return null;
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
