using UnityEngine;
using System.Collections.Generic;
using System;

public class JsonTset : MonoBehaviour
{

    public class Data
    {
        public string name = "test";
        private int age = 10;
        public List<string> usedNames = new List<string>() { "weixin", "liuxin" };
        public Close close = new Close();
        public v1 v = new v1() { a = 1 };
        public string str { get; set; }
    }
    [Serializable]
    public struct v1
    {
        public float a;
        public override string ToString()
        {
            return a.ToString();
        }
    }
    [Serializable]
    public class Close
    {
        public string Up = "上衣";
        public string Down = "裤子";
    }

    public Data data = new Data();
    // Use this for initialization
    void Start()
    {
        data.str = "sasdadsa";
        var str = JsonUtility.ToJson(data);
        Data d = JsonUtility.FromJson<Data>(str);
        print(str);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
