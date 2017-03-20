using UnityEngine;
using System.Collections;

public class ResourcesManager : Singleton<ResourcesManager>
{

    public T LoadAsset<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
}
