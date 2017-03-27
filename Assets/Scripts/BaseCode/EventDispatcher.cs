using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;
using System.Reflection;


public class EventDispatcher
{

}

public class EventData<T>
{
    public T type;
    public string name;
    public object obj;
    public MethodInfo MethodInfo
    {
        get
        {
            if (methodInfo == null)
            {
                Reflection();
            }
            return methodInfo;
        }
    }
    MethodInfo methodInfo;

    void Reflection()
    {
        methodInfo = obj.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }
}
public class EventDispatcher<T> : EventDispatcher
{

    Dictionary<T, List<EventData<T>>> events = new Dictionary<T, List<EventData<T>>>();
    public void AddListener(T type, object obj, string name)
    {
        List<EventData<T>> list;
        if (events.ContainsKey(type))
        {
            list = events[type];
        }
        else
        {
            list = new List<EventData<T>>();
            events[type] = list;
        }
        EventData<T> data = new EventData<T>();
        data.type = type;
        data.obj = obj;
        data.name = name;
        list.Add(data);
    }

    public void RemoveListenner(T type, object obj, string name)
    {
        List<EventData<T>> list;
        if (!events.ContainsKey(type)) return;
        list = events[type];
        for (int i = 0; i < list.Count; i++)
        {
            var data = list[i];
            if (data.obj.Equals(obj) && data.name.Equals(name))
            {
                list.RemoveAt(i);
            }
        }
        return;
    }

    public void Dispatch(T type, params object[] args)
    {
        List<EventData<T>> list;
        if (!events.ContainsKey(type)) return;
        list = events[type];
        for (int i = list.Count - 1; i > -1; i--)
        {
            var data = list[i];
            if (data.obj == null)
            {
                list.Remove(data);
                continue;
            }
            if (data.obj is MonoBehaviour)
            {
                var mon = data.obj as MonoBehaviour;
                if (mon == null)
                {
                    list.Remove(data);
                    continue;
                }
            }
            data.MethodInfo.Invoke(data.obj, args);
        }
    }

}
