using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour
{
    public const int maxHealth = 100;

    [SyncVar(hook = "HpChange")]
    public int health = maxHealth;

    void HpChange(int hp)
    {
        print(hp);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        //print("Damaged:" + amount);
        health -= amount;
        if (health <= 0)
        {
            health = maxHealth;
            RpcReSpanw();
        }
    }
    void RpcReSpanw()
    {
        transform.position = FindObjectOfType<NetworkManager>().startPositions[0].position;
    }
}

