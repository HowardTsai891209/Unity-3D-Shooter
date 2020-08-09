using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private float camRayLength = 100f;
    private int floorMask;
    public GameObject thirdPersonPlayer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = thirdPersonPlayer.GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor"); //拿到地板的layer
    }

    void Turning()
    { //角色轉向
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//主攝影機畫面內滑鼠的位置
        RaycastHit floorHit; //Ray的終點
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;//點到地板的位置 - 本身所在位置 = 一個向量
            //playerToMouse.y = 0;//不需要y
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);//旋轉角色
        }
    }
    void Move(float h, float v)//角色移動位置
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;//normalized 不取值 只取正負(方向)
        playerRigidbody.MovePosition(transform.position + movement);//移動位置
        Animating(h, v);//走路動畫
    }

    void Animating(float h, float v)
    {//角色走路動畫呼叫
        bool walking = (h != 0 || v != 0);//是否有走動
        anim.SetBool("isWalking", walking);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");    //左右
        float v = Input.GetAxis("Vertical");    //前後
        Move(h, v);
        Turning();
    }
}
