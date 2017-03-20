using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetWorkMoveTest : NetworkBehaviour
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
    float moveSpeed
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) ? traSpeed * 2 : traSpeed;
        }
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            rot = Quaternion.Euler(off);
            Cursor.lockState = CursorLockMode.Locked;
        }
        NetworkTransform nt;
        cc = GetComponent<CharacterController>();
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        Cursor.lockState = CursorLockMode.None;
    }
    private Vector3 off = new Vector3(30, 0, 0);
    private Vector3 dir;
    private Quaternion rot;
    // Update is called once per frame
    void Update()
    {

        // isLocalPlayer 是 NetworkBehaviour 的内置属性
        if (!isLocalPlayer)    //如果不是本地客户端，就返回，不执行下面的操作
        {
            return;
        }


        //transform.Translate(dir);   //朝某个方向移动
        //transform.Rotate(Vector3.up * h * rotSpeed * Time.deltaTime);  //围绕某轴旋转
        var mX = off.y + Input.GetAxis("Mouse X") * mouseSpeed;
        var mY = off.x - Input.GetAxis("Mouse Y") * mouseSpeed;
        mY = Mathf.Clamp(mY, -89, 89);
        off = new Vector3(mY, mX, 0);
        rot = Quaternion.Euler(off);

        disOff += Input.GetAxis("Mouse ScrollWheel") * mouseSpeed;

        transform.rotation = Quaternion.Euler(0, off.y, 0);
        if (cc.isGrounded)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
                return;
            var dot = Vector3.Dot(hit.normal, transform.up);
            if (dot < 0.55f)
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 10);
                dir = new Vector3(hit.normal.x, 0, hit.normal.z) * (1 + (1 - dot));
            }
            else
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                dir = transform.TransformDirection(new Vector3(h, 0, v)) * moveSpeed;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir.y = gSpeed;
            }
        }
        dir.y -= gravity * Time.deltaTime;
        cc.Move(dir * Time.deltaTime);
        if (Camera.main)
        {
            var pos = rot * new Vector3(0, 0, disOff) + transform.position;
            Camera.main.transform.position = pos;
            Camera.main.transform.LookAt(transform.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }

    void Test()
    {



    }

    [Command]
    void CmdFire()
    {
        var b = GameObject.Instantiate(bllute, transform.position + transform.forward, Quaternion.identity) as GameObject;
        b.GetComponent<Rigidbody>().velocity = transform.forward * bullteSpeed;
        NetworkServer.Spawn(b);
        Destroy(b, 2.0f);
    }
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
