using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class SignalBase
{
    protected Dictionary<Type, MethodInfo> events = new Dictionary<Type, MethodInfo>();

    protected List<Delegate> handlers = new List<Delegate>();

    protected virtual void AddListener(Delegate handler)
    {
        handlers.Add(handler);
    }

    protected virtual void RemoveListener(Delegate handler)
    {
        if (handlers.Contains(handler))
            handlers.Remove(handler);
    }

    public void Clear()
    {
        handlers.Clear();
    }

    protected virtual void Dispatch(params object[] args)
    {
        foreach (var h in handlers)
            Invoke(h, args);
    }

    protected void Invoke(Delegate handle, object[] args)
    {
        if (handle != null)
            handle.DynamicInvoke(args);
        //var type = handle.GetType();
        //MethodInfo m;
        //if (events.ContainsKey(type))
        //{
        //    m = events[type];
        //}
        //else
        //{
        //    m = type.GetMethod("Invoke");
        //    events[type] = m;
        //}
        //if (m != null)
        //    m.Invoke(handle, args);
    }

}
public class Signal : SignalBase
{
    public event Action handler;

    public void AddListener(Action handler)
    {
        base.AddListener(handler);
    }

    public void RemoveListener(Action handler)
    {
        base.RemoveListener(handler);
    }

    public void Dispatch()
    {
        base.Dispatch(null);
        if (handler != null) Invoke(handler, null);
    }
}

public class Signal<T> : SignalBase
{
    public event Action<T> handler;

    public void AddListener(Action<T> handler)
    {
        base.AddListener(handler);
    }

    public void RemoveListener(Action<T> handler)
    {
        base.RemoveListener(handler);
    }

    public void Dispatch(T arg)
    {
        base.Dispatch(arg);
        if (handler != null) Invoke(handler, new object[] { arg });
    }
}

public class Signal<T, T2> : SignalBase
{
    public void AddListener(Action<T, T2> handler)
    {
        base.AddListener(handler);
    }
    public void RemoveListener(Action<T, T2> handler)
    {
        base.RemoveListener(handler);
    }
    public void Dispatch(T arg, T2 arg2)
    {
        base.Dispatch(arg, arg2);
    }
}

public class Signal<T, T2, T3> : SignalBase
{

    public void AddListener(Action<T, T2, T3> handler)
    {
        base.AddListener(handler);
    }
    public void RemoveListener(Action<T, T2, T3> handler)
    {
        base.RemoveListener(handler);
    }
    public void Dispatch(T arg, T2 arg2, T3 arg3)
    {
        base.Dispatch(arg, arg2, arg3);
    }
}

public class Signal<T, T2, T3, T4> : SignalBase
{
    public void AddListener(Action<T, T2, T3, T4> handler)
    {
        base.AddListener(handler);
    }
    public void RemoveListener(Action<T, T2, T3, T4> handler)
    {
        base.RemoveListener(handler);
    }
    public void Dispatch(T arg, T2 arg2, T3 arg3, T4 arg4)
    {
        base.Dispatch(arg, arg2, arg3, arg4);
    }

}

public class test : Signal<string> { }

public class test2 : Signal<string, int> { }