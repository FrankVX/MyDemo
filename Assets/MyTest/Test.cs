using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    // Use this for initialization
    bool flag = false;
    Text text;
    void Awake()
    {
        Application.logMessageReceived += logMessageReceived;
        text = GetComponent<Text>();
        Debug.logger.logEnabled = false;
    }

    private void logMessageReceived(string condition, string stackTrace, LogType type)
    {
        string str = string.Format("condition:{0},stackTrace:{1},type:{2}", condition, stackTrace, type);
        text.text += str + "\n";
        // print(str+"---->print");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("test");
        }
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
