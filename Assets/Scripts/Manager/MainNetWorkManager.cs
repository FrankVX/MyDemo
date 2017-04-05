using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.NetworkSystem;

public class MainNetWorkManager : NetworkManager
{
    public GameManager gameManager { get { return GameManager.Instance; } }
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
            GameManager.Instance.Dispatch(ServerGlobalMsg.ServerStarted);
            ModuleMediatorManager.Instance.OnServerStart();
            NetMessageHandler.ServerStart();
        }
        if (!NetworkServer.active && isServerStart)
        {
            isServerStart = false;
            GameManager.Instance.Dispatch(ServerGlobalMsg.ServerStoped);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        gameManager.Dispatch(ServerGlobalMsg.OnStartServer);
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        gameManager.Dispatch(ClientGlobalMsg.OnStartClient);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if ((int)playerControllerId < conn.playerControllers.Count && conn.playerControllers[(int)playerControllerId].IsValid && conn.playerControllers[(int)playerControllerId].gameObject != null)
        {
            Debug.LogError("There is already a player at that playerControllerId for this connections.");
        }
        else
        {
            var prefab = gameManager.LoadAsset("Prefab/Cube");
            prefab.transform.position = new Vector3(10, 3, 10);
            GameObject player;
            player = gameManager.Spawn(prefab);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }


    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        GameManager.Instance.Dispatch(ClientGlobalMsg.OnClientConnect, conn);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        if (conn.isReady) return;
        base.OnServerReady(conn);
        GameManager.Instance.Dispatch(ClientGlobalMsg.OnClienReady, conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        print("OnClientConnect  isready :" + ClientScene.ready);
        base.OnClientConnect(conn);
        NetMessageHandler.ClientStart(client.connection);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        print("OnClientSceneChanged  isready :" + ClientScene.ready);
        if (ClientScene.ready) return;
        base.OnClientSceneChanged(conn);
    }

}
