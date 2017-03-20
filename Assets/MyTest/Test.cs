using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{

    // Use this for initialization
    bool flag = false;
    void Awake()
    {
        test2 t2 = new test2();
        GameObject ttt = new GameObject();
        var t = ttt.AddComponent<Test2>();
        t2.AddListener(t.sss);
        t2.Dispatch("sdsd", 4213);
        DestroyImmediate(ttt);
        t2.Dispatch("sdsd", 2233);
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {


    }
}

public class Test2 : MonoBehaviour
{
    public void sss(string str, int id)
    {
        transform.position = Vector3.one;
        print(str + id);
    }
}
