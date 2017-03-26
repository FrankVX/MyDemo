using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMoveController : MoveController
{
    public new GamePlayer Owner { get { return base.Owner as GamePlayer; } }
    void Update()
    {
        if (Owner.isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    NavMeshHit navHit;
                    if (NavMesh.SamplePosition(hit.point, out navHit, 10, -1))
                    {
                        CmdMove(navHit.position);
                    }
                }
            }
        }
    }

    [Command]
    void CmdMove(Vector3 pos)
    {
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(pos, out navHit, 10, -1))
        {
            RpcSyncMove(navHit.position);
        }
    }

}
