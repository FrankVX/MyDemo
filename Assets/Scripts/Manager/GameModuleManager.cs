using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameModuleManager : Singleton<GameModuleManager>
{
    Dictionary<Type, ClientModule> clientModules = new Dictionary<Type, ClientModule>();
    Dictionary<Type, ServerModule> serverModules = new Dictionary<Type, ServerModule>();
    public readonly NetworkHash128 assetid = NetworkHash128.Parse("GameModule");


    public override void Init()
    {
        base.Init();
        OnStartServer();
        OnStartClient();
        //AddListener(ServerGlobalMsg.OnClienReady, "OnClienReady");
        //AddListener(ServerGlobalMsg.OnStartServer, "OnStartServer");
        //AddListener(ClientGlobalMsg.OnStartClient, "OnStartClient");
    }

    void OnStartServer()
    {
        CreatModules<ServerModule>((t, o) => serverModules[t] = o);
    }

    void OnStartClient()
    {
        CreatModules<ClientModule>((t, o) => clientModules[t] = o);
    }

    private void CreatModules<T>(Action<Type, T> callBack) where T : MonoBehaviour
    {
        var types = GameManager.GetSubTypes<T>();
        foreach (var type in types)
        {
            GameObject obj = new GameObject(type.Name);
            obj.transform.SetParent(transform);
            if (callBack != null)
            {
                callBack(type, obj.AddComponent(type) as T);
            }
        }
    }

    GameObject SpawnGamemodule(Vector3 position, NetworkHash128 assetId)
    {
        GameObject obj = Instantiate(ResourcesManager.Instance.LoadAsset("Prefab/PlayerData"));
        obj.transform.parent = transform;
        return obj;
    }


    void OnClienReady(NetworkConnection conn)
    {
        if (NetworkServer.localClientActive) return;
        var obj = SpawnGamemodule(Vector3.zero, assetid);
        var data = obj.GetComponent<UserDataContainer>();
        data.Owner = conn;
        NetworkServer.SpawnWithClientAuthority(obj, assetid, conn);
    }

    public override T GetModule<T>()
    {
        var type = typeof(T);
        if (clientModules.ContainsKey(type))
        {
            return clientModules[type] as T;
        }
        else if (serverModules.ContainsKey(type))
        {
            return serverModules[type] as T;
        }
        return null;
    }


}
