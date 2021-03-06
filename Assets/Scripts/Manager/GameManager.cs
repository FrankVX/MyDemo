﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Reflection;
using System;
using System.Collections.Generic;

public class GameManager : GameBehaviour
{
    public static Assembly assembly { get; private set; }
    public static Type[] types { get; private set; }

    public static GameManager Instance;

    static GameManager()
    {
        assembly = Assembly.GetExecutingAssembly();
        types = assembly.GetTypes();
    }

    public static List<Type> GetSubTypes<T>()
    {
        List<Type> m_types = new List<Type>();
        var basetype = typeof(T);
        foreach (var type in types)
        {
            if (type.IsSubclassOf(basetype))
                m_types.Add(type);
        }
        return m_types;
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    protected override void Start()
    {
        Instance = this;
        base.Start();
        InitGame();
    }



    void InitGame()
    {
        //GetSingleton<SingletonManager>();
        GetSingleton<ResourcesManager>();
        GetSingleton<PoolSpawnManager>();
        GetSingleton<NetPrefabManager>();
        GetSingleton<GameModuleManager>();
        GetSingleton<ModuleMediatorManager>();
    }

}
