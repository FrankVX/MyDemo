using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class EventManager : Singleton<EventManager>
{

    Dictionary<Type, EventDispatcher> dispatchers = new Dictionary<Type, EventDispatcher>();

    public override void Init()
    {
        base.Init();
    }


    public EventDispatcher<T> GetDispatcher<T>()
    {
        var type = typeof(T);
        return GetDispatcher(type) as EventDispatcher<T>;
    }


    public EventDispatcher GetDispatcher(Type type)
    {
        if (dispatchers.ContainsKey(type))
        {
            return dispatchers[type];
        }
        else
        {
            if (type.IsDefined(typeof(EventTypeAttribute), false))
            {
                var dtype = typeof(EventDispatcher<>);
                dtype = dtype.MakeGenericType(type);
                EventDispatcher o = Activator.CreateInstance(dtype) as EventDispatcher;
                dispatchers[type] = o;
                return o;
            }
        }
        return null;
    }
}
