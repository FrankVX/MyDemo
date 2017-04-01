using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SpwanHandle
{
    public SpawnDelegate handle;
    public NetworkHash128 assetId;
    public GameObject prefab;
}

public class NetPrefabManager : Singleton<NetPrefabManager>
{
    Dictionary<NetworkHash128, SpwanHandle> alllPrefabs = new Dictionary<NetworkHash128, SpwanHandle>();

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
            var handle = new SpwanHandle() { assetId = ni.assetId, prefab = ni.gameObject };
            RegisterNetObj(handle);
        }
    }

    public void RegisterNetObj(SpwanHandle handle)
    {
        alllPrefabs[handle.assetId] = handle;
        ClientScene.RegisterSpawnHandler(handle.assetId, SpawnGameObject, UpSpawnGameObject);
        Debug.Log(string.Format("RegisterNetObj,assid ={0}", handle.assetId));
    }

    GameObject SpawnGameObject(Vector3 pos, NetworkHash128 assetId)
    {
        Debug.Log(string.Format("SpawnGameObject01,assid ={0}", assetId));
        var handle = alllPrefabs[assetId];
        GameObject obj = null;
        if (handle.handle != null)
        {
            obj = handle.handle(pos, assetId);
        }
        if (obj == null && handle.prefab != null)
        {
            obj = PoolSpawnManager.Instance.Spawn(alllPrefabs[assetId].prefab);
            if (obj) obj.transform.parent = transform;
        }
        Debug.Log(string.Format("SpawnGameObject02,assid ={0} name={1}", assetId, obj.name));
        return obj;
    }

    void UpSpawnGameObject(GameObject obj)
    {
        PoolSpawnManager.Instance.DeSpawn(obj);
    }
}
