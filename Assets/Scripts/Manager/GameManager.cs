using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Reflection;
using System;

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
    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        InitGame();
        StartGame();
    }

    void StartGame()
    {
        gameObject.AddComponent<NetworkManagerHUD>();
    }

    void InitGame()
    {
        GetSingleton<SingletonManager>();
        GetSingleton<ResourcesManager>();
        GetSingleton<PoolSpawnManager>();
        GetSingleton<NetPrefabManager>();
        GetSingleton<GameModuleManager>();
        gameObject.AddComponent<MainNetWorkManager>().gameManager = this;
    
    }

}
