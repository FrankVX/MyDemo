using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.NetworkSystem;

public class MainNetWorkManager : NetworkManager
{
    public GameManager gameManager;
    public NetworkIdentity id;

    bool flag;
    bool isServerStart;
    NetMessageHandler test;
    GameObject obj;

    void Awake()
    {
        onlineScene = "game";
        obj = ResourcesManager.Instance.LoadAsset("Prefab/module");
        ClientScene.RegisterPrefab(obj);
    }

    private void Update()
    {
        if (NetworkServer.active && !isServerStart)
        {
            isServerStart = true;
            GameManager.Instance.Dispatch(ServerGlobalMsg.ServerStart);
            test = new NetMessageHandler();
        }
        if (!NetworkServer.active && isServerStart)
        {
            isServerStart = false;
            GameManager.Instance.Dispatch(ServerGlobalMsg.ServerStop);
        }
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var obj = gameManager.CreatInstance("Prefab/Cube");
        obj.transform.position = new Vector3(10, 3, 10);
        NetworkServer.AddPlayerForConnection(conn, obj, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        var module = Instantiate(obj) as GameObject;
        NetworkServer.SpawnWithClientAuthority(module, conn);
    }
}
