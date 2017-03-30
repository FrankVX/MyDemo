using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ModuleMediatorManager : Singleton<ModuleMediatorManager>
{

    Dictionary<NetworkHash128, ModuleMediator> mediators = new Dictionary<NetworkHash128, ModuleMediator>();


    public override void Init()
    {
        base.Init();
        CreatMediator();
        RegisterMediator();
    }

    public void OnServerStart()
    {
        foreach (var m in mediators)
        {
            NetworkServer.Spawn(m.Value.gameObject, m.Key);
        }
    }

    private void CreatMediator()
    {
        var types = GameManager.GetSubTypes<ModuleMediator>();
        foreach (var type in types)
        {
            GameObject obj = new GameObject(type.Name);
            obj.transform.SetParent(transform);
            var mediator = obj.AddComponent(type) as ModuleMediator;
            NetworkHash128 id = NetworkHash128.Parse(type.FullName);
            mediators[id] = mediator;
        }
    }


    private void RegisterMediator()
    {
        foreach (var obj in mediators)
        {
            SpwanHandle handle = new SpwanHandle();
            handle.assetId = obj.Key;
            handle.handle = OnSpawnMediator;
            NetPrefabManager.Instance.RegisterNetObj(handle);
            Dispatch(ChatEvent.ShowChat, string.Format("RegisterMediator name={0}", obj.Value.gameObject.name));
        }
    }

    GameObject OnSpawnMediator(Vector3 pos, NetworkHash128 assetid)
    {
        ModuleMediator m;
        if (mediators.TryGetValue(assetid, out m))
        {
            return m.gameObject;
        }
        return null;
    }


    public override T GetMediator<T>()
    {
        var type = typeof(T);
        NetworkHash128 id = NetworkHash128.Parse(type.FullName);
        if (mediators.ContainsKey(id))
            return mediators[id] as T;
        return null;
    }
}
