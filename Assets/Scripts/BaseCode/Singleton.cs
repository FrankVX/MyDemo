using UnityEngine;
using System.Collections;



public class Singleton : GameBehaviour
{
    public virtual bool DontDestroy { get { return true; } }
    public virtual HideFlags HideFlag { get { return HideFlags.None; } }
    public virtual void Init() { }

}


public class Singleton<T> : Singleton where T : Singleton<T>
{
    public static T Instance
    {
        get
        {
            return SingletonManager.Instance.GetInstance<T>();
        }
    }
}
