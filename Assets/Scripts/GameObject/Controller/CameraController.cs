using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : BaseController
{
    public Vector3 angle = new Vector3(45, 0, 0);
    public float distance = 15;
    public float speed = 10;
    Camera mcamera { get { return Camera.main; } }


    [ClientCallback]
    private void LateUpdate()
    {
        if (!isLocalPlayer) return;
        Quaternion q = Quaternion.Euler(angle);
        var target = transform.position + q * Vector3.back * distance;
        var rot = Quaternion.LookRotation(transform.position - target);
        mcamera.transform.position = Vector3.Lerp(mcamera.transform.position, target, speed * Time.deltaTime);
        mcamera.transform.rotation = Quaternion.Lerp(mcamera.transform.rotation, rot, speed * Time.deltaTime);
    }


}
