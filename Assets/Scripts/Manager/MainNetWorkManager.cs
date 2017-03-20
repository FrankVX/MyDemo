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
    private void Update()
    {
        if (NetworkServer.active && !isServerStart)
        {
            isServerStart = true;
            GameManager.Instance.Dispatch(GameMsgType.ServerStart);
            test = new NetMessageHandler();
        }
        if (!NetworkServer.active && isServerStart)
        {
            isServerStart = false;
            GameManager.Instance.Dispatch(GameMsgType.ServerStop);
        }
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var obj = gameManager.CreatInstance("Prefab/Cube");
        obj.transform.position = new Vector3(10, 3, 10);
        NetworkServer.AddPlayerForConnection(conn, obj, playerControllerId);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        NetworkServer.ClearHandlers();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        print("OnServerConnect");
        //conn.Send(MsgType.Scene, new StringMessage("Game"));
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        print("OnClientConnect");
        //base.OnClientConnect(conn);

        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        //SceneManager.LoadSceneAsync("Game");
        ClientScene.Ready(this.client.connection);
        ClientScene.AddPlayer(0);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        print(arg0.name);

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        print("OnClientDisconnect");
        base.OnClientDisconnect(conn);
        // SceneManager.LoadSceneAsync("Login");
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        print("OnClientSceneChanged");
        base.OnClientSceneChanged(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        print("OnServerDisconnect");
        base.OnServerDisconnect(conn);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        print("OnServerReady");
        //base.OnServerReady(conn);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        print("OnServerSceneChanged");
        base.OnServerSceneChanged(sceneName);
    }

    public override void OnStartServer()
    {
        print("OnStartServer");
        base.OnStartServer();

    }

    public override void OnStartClient(NetworkClient client)
    {
        print("OnStartClient");
        base.OnStartClient(client);
    }
    public override void OnClientNotReady(NetworkConnection conn)
    {
        print("OnClientNotReady");
        base.OnClientNotReady(conn);
    }

    public override void OnStartHost()
    {
        print("OnStartHost");
        base.OnStartHost();
    }
}
