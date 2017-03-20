using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetPrefabManager : Singleton<NetPrefabManager>
{
    Dictionary<NetworkHash128, Object> alllPrefabs = new Dictionary<NetworkHash128, Object>();

    public override void Init()
    {
        base.Init();
        LoadAllResources();
    }

    void LoadAllResources()
    {
        Resources.UnloadUnusedAssets();
        var objs = Resources.LoadAll<NetworkIdentity>("Prefab");
        foreach (var obj in objs)
        {
            var ni = obj as NetworkIdentity;
            ClientScene.RegisterSpawnHandler(ni.assetId, SpawnGameObject, UpSpawnGameObject);
            alllPrefabs[ni.assetId] = ni.gameObject;
        }
    }

    GameObject SpawnGameObject(Vector3 pos, NetworkHash128 assetId)
    {
        return PoolSpawnManager.Instance.Spawn(alllPrefabs[assetId] as GameObject);
    }

    void UpSpawnGameObject(GameObject obj)
    {
        PoolSpawnManager.Instance.DeSpawn(obj);
    }
}
