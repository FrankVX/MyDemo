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
            if (instance == null)
                instance = SingletonManager.Instance.GetInstance<T>();
            return instance;
        }
    }

    private static T instance;

    protected override void Awake()
    {
        base.Awake();
        instance = this as T;
        SingletonManager.Instance.RegistSingleton(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        instance = null;
    }
}
