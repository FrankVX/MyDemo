using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameBehaviour : MonoBehaviour
{
    public virtual T GetSingleton<T>() where T : Singleton
    { return SingletonManager.Instance.GetInstance<T>(); }
    public virtual T GetModule<T>() where T : GameBehaviour
    { return GameModuleManager.Instance.GetModule<T>(); }
    public virtual T GetMediator<T>() where T : ModuleMediator
    { return ModuleMediatorManager.Instance.GetMediator<T>(); }

    public GameObject CreatInstance(string path) { return Spawn(LoadAsset(path)); }

    public GameObject LoadAsset(string path) { return ResourcesManager.Instance.LoadAsset<GameObject>(path); }

    public GameObject Spawn(GameObject prefab) { return PoolSpawnManager.Instance.Spawn(prefab); }

    public void DeSpawn(GameObject prefab) { PoolSpawnManager.Instance.DeSpawn(prefab); }

    public static NetworkClient Clien { get { return NetworkManager.singleton.client; } }

    public void AddListener<T>(T type, string name)
    {
        EventManager.Instance.GetDispatcher<T>().AddListener(type, this, name);
    }

    public T GetSignal<T>() where T : SignalBase
    {
        return SignalManager.Instance.GetSignal<T>();
    }

    public void RemoveListener<T>(T type, string name)
    {
        EventManager.Instance.GetDispatcher<T>().RemoveListenner(type, this, name);
    }

    public void Dispatch<T>(T type, params object[] args)
    {
        EventManager.Instance.GetDispatcher<T>().Dispatch(type, args);
    }

    protected virtual void Awake()
    {

    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void OnDisable()
    {

    }
    protected virtual void OnDestroy()
    {

    }

}
