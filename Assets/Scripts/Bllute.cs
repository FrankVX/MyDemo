using UnityEngine;
using System.Collections;

public class Bllute : MonoBehaviour
{
    Vector3 prePos;
    Vector3 startPos;
    private void Start()
    {
        startPos = prePos = transform.position;
    }
    private void Update()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(startPos, transform.forward,out hit ,Vector3.Distance(startPos, transform.position)))
        //{
        //    Enter(hit.transform.gameObject);
        //}


        prePos = transform.position;
    }

    void Enter(GameObject hit)
    {
        var combat = hit.GetComponent<Combat>();
        if (combat != null)
        {
            combat.TakeDamage(10);
        }
        Destroy(gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        Enter(hit);
    }
}
