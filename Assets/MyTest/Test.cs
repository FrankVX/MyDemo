using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{

    // Use this for initialization
    bool flag = false;
    void Awake()
    {


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
