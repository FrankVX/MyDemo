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

    private void RegistNetHandler()
    {
        var ms = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        Handler handler = new Handler();
        handler.obj = this;
        foreach (var m in ms)
        {
            if (m.IsDefined(typeof(RemoteAttribute), true))
            {
                handler.AddMethod(m);
            }
        }
        if (handler.methodInfos.Count > 0)
            NetMessageHandler.RegsiterHandler(handler);
    }
}
