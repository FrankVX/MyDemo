using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameObj : GameNetBehaviour
{
    Dictionary<Type, BaseController> controllers = new Dictionary<Type, BaseController>();
    List<Type> mTypes = new List<Type>();


    public virtual void InitControllers() { }


    public virtual void CreatControllers()
    {
        foreach (var t in mTypes)
        {
            if (controllers.ContainsKey(t)) continue;
            var com = gameObject.AddComponent(t) as BaseController;
            controllers[t] = com;
            com.Init(this);
        }
    }

    public virtual void AddController<T>() where T : BaseController
    {
        var type = typeof(T);
        if (mTypes.Contains(type))
        {
            Debug.LogError("Oready have component:" + type.Name);
            return;
        }
        mTypes.Add(type);

    }

    public virtual T GetController<T>() where T : BaseController
    {
        var type = typeof(T);
        if (!controllers.ContainsKey(type))
        {
            foreach (var c in controllers)
            {
                if (c.Value.GetType().IsSubclassOf(type))
                {
                    return c.Value as T;
                }
            }
            Debug.LogError("Not have component:" + type.Name);
            return null;
        }
        return controllers[type] as T;
    }

    public virtual void RemoveController<T>()
    {
        var type = typeof(T);
        if (!mTypes.Contains(type))
        {
            Debug.LogError("Not have component:" + type.Name);
            return;
        }
        mTypes.Remove(type);
        if (controllers.ContainsKey(type))
        {
            var obj = controllers[type];
            controllers.Remove(type);
            Destroy(obj);
        }
    }

}
