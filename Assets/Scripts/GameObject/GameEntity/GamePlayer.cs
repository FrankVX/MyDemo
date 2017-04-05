using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public struct buff
{
    public int id;
}

public class Buffs : SyncListStruct<buff>
{

}


public class GamePlayer : GameActor
{
    [SyncVar]
    public string UserName;

    [SyncVar]
    public Buffs buffs = new Buffs();

    public override void InitControllers()
    {
        base.InitControllers();
        RemoveController<MoveController>();
        AddController<PlayerMoveController>();
        AddController<CameraController>();
    }





    //========================================ServerCode================================

    
}
