using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameActor : GameEntity
{
    [SyncVar]
    public int Hp;
    [SyncVar]
    public int Mp;

    public override void InitControllers()
    {
        base.InitControllers();
        AddController<MoveController>();
    }

    protected virtual void OnSyncMove(Vector3 pos)
    {
        GetController<MoveController>().MoveToPostion(pos);
    }



    //=============================================OnClient===========================

}
