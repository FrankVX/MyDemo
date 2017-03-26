using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoveController : BaseController
{
    [SyncVar(hook = "UpdateSpeed")]
    public float Speed;

    public new GameActor Owner { get { return base.Owner as GameActor; } }

    protected NavMeshAgent nma;
    public override void Init(GameObj owner)
    {
        base.Init(owner);
        nma = Owner.GetComponent<NavMeshAgent>();
        if (!nma) nma = Owner.gameObject.AddComponent<NavMeshAgent>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        UpdateSpeed(Speed);
    }

    private void UpdateSpeed(float value)
    {
        Speed = value;
        nma.speed = Speed;
        nma.angularSpeed = Speed * 100;
        nma.acceleration = Speed * 10;
    }

    public override void OnRemove()
    {
        base.OnRemove();
        nma = null;
    }

    public virtual void MoveToPostion(Vector3 pos)
    {
        nma.SetDestination(pos);
    }

    [ClientRpc]
    protected void RpcSyncMove(Vector3 pos)
    {
        MoveToPostion(pos);
    }

    //==================================Server================================
    public override void OnStartServer()
    {
        base.OnStartServer();
        Speed = 10;
    }
}
