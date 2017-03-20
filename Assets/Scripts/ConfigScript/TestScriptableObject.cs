using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

[CreateAssetMenu()]
public class TestScriptableObject : ScriptableObject
{

    public bool globle1 = false;
    public int globle2 = 1;
    public GlobleType globleType;
    public Vector3 v3;
    public Transform trans;
    public Texture texture;
    public GamePlayer player;

    public enum GlobleType
    {
        type1,
        type2,
        type3
    }
}
