using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<T> : Singleton<T> where T : Manager<T>
{
    Dictionary<object, object> modules = new Dictionary<object, object>();



}
