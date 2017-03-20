using UnityEngine;
using System.Collections.Generic;

public class PoolSpawnManager : Singleton<PoolSpawnManager>
{
    Dictionary<GameObject, PoolPrefab> poolPrefabs = new Dictionary<GameObject, PoolPrefab>();
    Dictionary<GameObject, PoolPrefab> spawnedPrefabs = new Dictionary<GameObject, PoolPrefab>();
    public new GameObject Spawn(GameObject prefab)
    {
        PoolPrefab pp;
        if (poolPrefabs.ContainsKey(prefab))
        {
            pp = poolPrefabs[prefab];
        }
        else
        {
            pp = CreatPoolPrefab(prefab);
        }
        var obj = pp.Spawn();
        spawnedPrefabs[obj] = pp;
        return obj;
    }

    public new void DeSpawn(GameObject obj)
    {
        if (spawnedPrefabs.ContainsKey(obj))
        {
            var pp = spawnedPrefabs[obj];
            spawnedPrefabs.Remove(obj);
            pp.DeSpawn(obj);
        }
    }

    PoolPrefab CreatPoolPrefab(GameObject prefab)
    {
        PoolPrefab pp = new GameObject(prefab.name).AddComponent<PoolPrefab>();
        pp.Init(prefab);
        pp.transform.parent = transform;
        pp.transform.localPosition = Vector3.zero;
        poolPrefabs[prefab] = pp;
        return pp;
    }

    public override HideFlags HideFlag
    {
        get
        {
            return HideFlags.None;
        }
    }
}

public class PoolPrefab : GameBehaviour
{
    public GameObject Prefab { get; set; }
    List<GameObject> prefabs = new List<GameObject>();

    public void Init(GameObject prefab)
    {
        Prefab = prefab;
        CreatPrefab();
    }
    public GameObject Spawn()
    {
        GameObject obj;
        if (prefabs.Count > 0) obj = prefabs[0];
        else obj = CreatPrefab();
        prefabs.Remove(obj);
        obj.transform.parent = null;
        obj.SetActive(true);
        return obj;
    }

    public new void DeSpawn(GameObject obj)
    {
        if (!obj) return;
        obj.SetActive(false);
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        prefabs.Add(obj);
    }

    GameObject CreatPrefab()
    {
        var obj = GameObject.Instantiate(Prefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
        var coms = obj.GetComponents<GameObj>();
        for(int i=0;i<coms.Length;i++)
        {
            coms[i].InitControllers();
            coms[i].CreatControllers();
        }
        prefabs.Add(obj);
        return obj;
    }
}
