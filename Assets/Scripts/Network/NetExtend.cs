using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public static class NetExtend
{

    public static void WriteData(this NetworkWriter writer, object[] args)
    {
        foreach (var arg in args)
        {
            if (arg is int)
            {
                writer.Write((int)arg);
            }
            else if (arg is uint)
            {
                writer.Write((uint)arg);
            }
            else if (arg is string)
            {
                writer.Write((string)arg);
            }
            else if (arg is float)
            {
                writer.Write((float)arg);
            }
            else if (arg is long)
            {
                writer.Write((long)arg);
            }
            else if (arg is ulong)
            {
                writer.Write((ulong)arg);
            }
            else if (arg is byte)
            {
                writer.Write((byte)arg);
            }
            else if (arg is char)
            {
                writer.Write((char)arg);
            }
            else if (arg is bool)
            {
                writer.Write((bool)arg);
            }
            else if (arg is Vector3)
            {
                writer.Write((Vector3)arg);
            }
            else if (arg is Vector2)
            {
                writer.Write((Vector2)arg);
            }
            else if (arg is NetworkHash128)
            {
                writer.Write((NetworkHash128)arg);
            }
            else if (arg is MessageBase)
            {
                (arg as MessageBase).Serialize(writer);
            }
        }
    }


    public static object[] ReadData(this NetworkReader reader, NetworkConnection sender, MethodInfo method)
    {
        if (reader == null || method == null) return null;
        var ps = method.GetParameters();
        object[] args = new object[ps.Length];
        for (int i = 0; i < ps.Length; i++)
        {
            var pt = ps[i].ParameterType;
            if (pt.Equals(typeof(NetworkConnection)))
            {
                args[i] = sender;
            }
            else if (pt.Equals(typeof(int)))
            {
                args[i] = reader.ReadInt32();
            }
            else if (pt.Equals(typeof(uint)))
            {
                args[i] = reader.ReadUInt32();
            }
            else if (pt.Equals(typeof(float)))
            {
                args[i] = reader.ReadSingle();
            }
            else if (pt.Equals(typeof(string)))
            {
                args[i] = reader.ReadString();
            }
            else if (pt.Equals(typeof(long)))
            {
                args[i] = reader.ReadInt64();
            }
            else if (pt.Equals(typeof(ulong)))
            {
                args[i] = reader.ReadUInt64();
            }
            else if (pt.Equals(typeof(byte)))
            {
                args[i] = reader.ReadByte();
            }
            else if (pt.Equals(typeof(char)))
            {
                args[i] = reader.ReadChar();
            }
            else if (pt.Equals(typeof(bool)))
            {
                args[i] = reader.ReadBoolean();
            }
            else if (pt.Equals(typeof(Vector3)))
            {
                args[i] = reader.ReadVector3();
            }
            else if (pt.Equals(typeof(Vector2)))
            {
                args[i] = reader.ReadVector2();
            }
            else if (pt.Equals(typeof(Transform)))
            {
                args[i] = reader.ReadTransform();
            }
            else if (pt.Equals(typeof(Quaternion)))
            {
                args[i] = reader.ReadQuaternion();
            }
            else if (pt.Equals(typeof(NetworkInstanceId)))
            {
                args[i] = reader.ReadNetworkId();
            }
            else if (pt.Equals(typeof(NetworkIdentity)))
            {
                args[i] = reader.ReadNetworkIdentity();
            }
            else if (pt.Equals(typeof(NetworkHash128)))
            {
                args[i] = reader.ReadNetworkHash128();
            }
            else if (pt.Equals(typeof(GameObject)))
            {
                args[i] = reader.ReadGameObject();
            }
            else if (pt.IsSubclassOf(typeof(MessageBase)))
            {
                var m = Activator.CreateInstance(pt) as MessageBase;
                m.Deserialize(reader);
                args[i] = m;
            }
        }
        return args;
    }
}
