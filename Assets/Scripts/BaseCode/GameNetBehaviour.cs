﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GameNetBehaviour : NetworkBehaviour
{
    public NetworkIdentity netIdentity
    {
        get
        {
            if (!_netIdentity) _netIdentity = GetComponent<NetworkIdentity>();
            return _netIdentity;
        }
    }
    private NetworkIdentity _netIdentity;
    public T GetSingleton<T>() where T : Singleton
    { return SingletonManager.Instance.GetInstance<T>(); }
    public T GetModule<T>() where T : BaseGameModule
    { return GameModuleManager.Instance.GetModule<T>(); }

    public T GetSignal<T>() where T : SignalBase
    {
        return SignalManager.Instance.GetSignal<T>();
    }
    public void AddListener<T>(T type, string name)
    {
        EventManager.Instance.GetDispatcher<T>().AddListener(type, this, name);
    }

    public void RemoveListener<T>(T type, string name)
    {
        EventManager.Instance.GetDispatcher<T>().RemoveListenner(type, this, name);
    }

    public void Dispatch<T>(T type, params object[] args)
    {
        EventManager.Instance.GetDispatcher<T>().Dispatch(type, args);
    }

    public GameObject CreatInstance(string path) { return Spawn(LoadAsset(string.Concat("Prefab/", path))); }

    public GameObject LoadAsset(string path) { return ResourcesManager.Instance.LoadAsset<GameObject>(path); }

    public GameObject Spawn(GameObject prefab) { return PoolSpawnManager.Instance.Spawn(prefab); }

    public void DeSpawn(GameObject prefab) { PoolSpawnManager.Instance.DeSpawn(prefab); }

    public static NetworkClient Clien { get { return NetworkManager.singleton.client; } }

    protected virtual void Awake()
    {

    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void OnDisable()
    {

    }
    protected virtual void OnDestroy()
    {

    }

}
