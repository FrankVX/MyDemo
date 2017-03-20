using UnityEngine;
using System.Collections;

public class BaseController : GameNetBehaviour
{
    public BaseController() { }
    public GameObj Owner { get; private set; }

    public virtual void Init(GameObj owner)
    {
        Owner = owner;
    }

    public virtual void OnRemove()
    {

    }

}
