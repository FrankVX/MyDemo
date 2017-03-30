


using UnityEngine;

namespace MC.CheatNs
{
    public interface ICheatDetectable
    {
        string Name { get; }
        GameObject gameObject { get; }
    }
}