using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MyNetWorkManager : NetworkManager
{
    Dictionary<NetworkHash128, Object> alllPrefabs = new Dictionary<NetworkHash128, Object>();

    private void Start()
    {
        //StartHost();
        LoadAllResources();

    }

    void LoadAllResources()
    {
        Resources.UnloadUnusedAssets();
        //var objs = Resources.FindObjectsOfTypeAll(typeof(NetworkIdentity));
        var objs = Resources.LoadAll<NetworkIdentity>("Prefab");
        foreach (var obj in objs)
        {
            print(obj.name);
            var ni = obj as NetworkIdentity;
            var ha = NetworkHash128.Parse(ni.assetId.ToString());
            
            ClientScene.RegisterSpawnHandler(ni.assetId, SpawnGameObjects, UpSpawnGameObject);
            alllPrefabs[ni.assetId] = ni.gameObject;
        }
    }

    GameObject SpawnGameObjects(Vector3 pos, NetworkHash128 assetId)
    {
        print("SpawnGameObjects assetId : " + assetId);
        return Instantiate(alllPrefabs[assetId], pos, Quaternion.identity) as GameObject;
    }

    void UpSpawnGameObject(GameObject obj)
    {
        print("UpSpawnGameObject  name ;  " + obj.name);
        Destroy(obj);
    }
}
