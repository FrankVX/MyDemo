using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameModuleManager : Singleton<GameModuleManager>
{
    Dictionary<Type, BaseGameModule> modules = new Dictionary<Type, BaseGameModule>();
    public readonly NetworkHash128 assetid = NetworkHash128.Parse("GameModule");
    public override void Init()
    {
        base.Init();
        CreatModules();
        RegisterModeles();
        AddListener(ServerGlobalMsg.ServerStart, "OnServerStart");
        AddListener(ServerGlobalMsg.OnClienReady, "OnClienReady");
        //ClientScene.RegisterSpawnHandler(assetid, SpawnGamemodule, UnSpawnGamemodule);
    }

    GameObject SpawnGamemodule(Vector3 position, NetworkHash128 assetId)
    {
        GameObject obj = Instantiate(ResourcesManager.Instance.LoadAsset("Prefab/PlayerData"));
        obj.transform.parent = transform;
        return obj;
    }

    void UnSpawnGamemodule(GameObject obj)
    {

    }

    void OnServerStart()
    {
        foreach (var m in modules.Values)
        {
            NetworkServer.Spawn(m.gameObject);
        }
    }


    void OnClienReady(NetworkConnection conn)
    {
        var obj = SpawnGamemodule(Vector3.zero, assetid);
        var data = obj.GetComponent<UserDataContainer>();
        data.Owner = conn;
        NetworkServer.SpawnWithClientAuthority(obj, assetid, conn);
    }


    private void CreatModules()
    {
        var types = GameManager.GetSubTypes<BaseGameModule>();
        foreach (var type in types)
        {
            GameObject obj = new GameObject(type.Name);
            obj.transform.SetParent(transform);
            var module = obj.AddComponent(type) as BaseGameModule;
            modules[type] = module;
        }
    }

    private void RegisterModeles()
    {
        foreach (var obj in modules)
        {
            ClientScene.RegisterPrefab(obj.Value.gameObject, NetworkHash128.Parse(obj.Key.Name));
        }
    }

    public new T GetModule<T>() where T : BaseGameModule
    {
        var type = typeof(T);
        if (modules.ContainsKey(type))
            return modules[type] as T;
        return null;
    }


}
