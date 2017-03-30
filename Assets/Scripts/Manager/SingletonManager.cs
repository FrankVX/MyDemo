using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class SingletonManager : Singleton<SingletonManager>
{
    public static new SingletonManager Instance
    {
        get
        {
            if (instance == null)
                instance = CreatInstance<SingletonManager>();
            return instance;
        }
    }
    static SingletonManager instance;

    protected override void Awake()
    {
        base.Init();
    }



    Dictionary<Type, Singleton> instances = new Dictionary<Type, Singleton>();
    public T GetInstance<T>() where T : Singleton
    {
        T value;
        var type = typeof(T);
        if (instances.ContainsKey(type))
        {
            value = instances[type] as T;
        }
        else
        {
            value = CreatInstance<T>();
            value.Init();
            instances[type] = value;
        }
        return value;
    }


    static T CreatInstance<T>() where T : Singleton
    {
        T instance = new GameObject(typeof(T).Name).AddComponent<T>();
        if (instance.DontDestroy)
            GameObject.DontDestroyOnLoad(instance.gameObject);
        instance.gameObject.hideFlags = instance.HideFlag;
        return instance;
    }



}
