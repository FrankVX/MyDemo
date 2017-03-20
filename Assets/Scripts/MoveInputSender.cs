using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoveInputSender : NetworkBehaviour
{
    public float traSpeed = 6, rotSpeed = 120;
    public CharacterController cc;
    public float mouseSpeed = 2;
    public float gSpeed = 10;
    public float gravity = 1;
    public float dalye = 2;
    public GameObject bllute;
    public float bullteSpeed = 50;
    float disOff = -5;
    private Vector3 off = new Vector3(30, 0, 0);
    private Vector3 dir;
    private Vector3 dir2;
    Vector3 sdir;
    private Quaternion rot;
    bool isjump;
    float moveSpeed
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) ? traSpeed * 2 : traSpeed;
        }
    }


    private void Update()
    {
        if (isServer)
        {
            if (cc.isGrounded)
            {
                cc.enableOverlapRecovery = true;
                sdir = new Vector3(dir2.x, 0, dir2.z);
                if (isjump)
                {
                    sdir.y = gSpeed;
                    isjump = false;
                }
            }

            sdir.y -= gravity * Time.deltaTime;
            cc.Move(sdir * Time.deltaTime);
        }
       


        if (isLocalPlayer)
        {

            var mX = off.y + Input.GetAxis("Mouse X") * mouseSpeed;
            var mY = off.x - Input.GetAxis("Mouse Y") * mouseSpeed;
            mY = Mathf.Clamp(mY, -89, 89);
            off = new Vector3(mY, mX, 0);
            rot = Quaternion.Euler(off);

            disOff += Input.GetAxis("Mouse ScrollWheel") * mouseSpeed;


            Vector3 d = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) d += rot*transform.forward;
            if (Input.GetKey(KeyCode.S)) d -= rot*transform.forward;
            if (Input.GetKey(KeyCode.A)) d -= rot*transform.right;
            if (Input.GetKey(KeyCode.D)) d += rot*transform.right;

            //d *= moveSpeed;

            //float h = Input.GetAxis("Horizontal");
            //float v = Input.GetAxis("Vertical");
            //var d = transform.TransformDirection(new Vector3(h, 0, v)) * moveSpeed;

            bool jump = Input.GetKeyDown(KeyCode.Space);
            if (Vector3.Distance(d, dir) > 0.1f || jump)
            {
                dir = d;
                CmdMove(dir, jump);
                print("send");
            }

            if (Camera.main)
            {
                var pos = rot * new Vector3(0, 0, disOff) + transform.position;
                Camera.main.transform.position = pos;
                Camera.main.transform.LookAt(transform.position);
            }
        }
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        dir.y = gSpeed;
        //    }
        //}
        //dir.y -= gravity * Time.deltaTime;






    }

    [Command]
    void CmdMove(Vector3 dir, bool isJump)
    {
        this.dir2 = dir.normalized * moveSpeed;
        this.isjump = isJump;
    }

}
