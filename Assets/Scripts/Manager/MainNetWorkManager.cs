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

    private void Awake()
    {
        onlineScene = "game";
    }

    private void Update()
    {
        if (NetworkServer.active && !isServerStart)
        {
            isServerStart = true;
            GameManager.Instance.Dispatch(ServerGlobalMsg.ServerStart);
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


    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        GameManager.Instance.Dispatch(ServerGlobalMsg.OnClientConnect, conn);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        GameManager.Instance.Dispatch(ServerGlobalMsg.OnClienReady, conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        print("OnClientConnect");
        base.OnClientConnect(conn);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        print("OnClientSceneChanged");
        ClientScene.Ready(conn);
        //if (!NetworkServer.active) ClientScene.AddPlayer(0);
        ClientScene.AddPlayer(0);

    }

}
