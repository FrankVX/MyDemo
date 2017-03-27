using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameModuleManager : Singleton<GameModuleManager>
{
    Dictionary<Type, BaseGameModule> modules = new Dictionary<Type, BaseGameModule>();
    public override void Init()
    {
        base.Init();
        AddListener(ServerGlobalMsg.ServerStart, "OnServerStart");
    }

    private void OnServerStart()
    {
        CreatModules(typeof(ServerModule));
        SpwanModule();
    }


    void OnClientStart()
    {
        CreatModules(typeof(ClientModule));
        SpwanModule();
    }

    void SpwanModule()
    {
        foreach (var obj in modules)
        {
            NetworkServer.Spawn(obj.Value.gameObject, NetworkHash128.Parse(obj.Key.Name));
        }
    }


    private void CreatModules(Type moduleType)
    {
        for (int i = 0; i < GameManager.types.Length; i++)
        {
            var type = GameManager.types[i];
            if (type.IsSubclassOf(moduleType))
            {
                BaseGameModule instance = new GameObject(type.Name).AddComponent(type) as BaseGameModule;
                DontDestroyOnLoad(instance.gameObject);
                instance.transform.parent = transform;
                modules.Add(type, instance);
            }
        }
        RegisterModeles();
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
