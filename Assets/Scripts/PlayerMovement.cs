using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private float camRayLength = 100f;
    private int floorMask;
    private Vector3 velocity;
    public GameObject FollowCamera;
    public ThirdCamFollow thirdCamFollow;
    private Vector3 velo;
    private int switcher = 0;
    private float counter;

    void Awake(){
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor"); //拿到地板的layer
    }

    void Turning(){ //角色轉向
        if (switcher % 2 == 1)
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//主攝影機畫面內滑鼠的位置
            RaycastHit floorHit; //Ray的終點
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;//點到地板的位置 - 本身所在位置 = 一個向量
                playerToMouse.y = 0;//不需要y
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidbody.MoveRotation(newRotation);//旋轉角色
            }
        }      
    }
    void Move(float h, float v)//角色移動位置
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;//normalized 不取值 只取正負(方向)
        playerRigidbody.MovePosition(transform.position + movement);//移動位置
        Animating(h, v);//走路動畫

        /*counter += Time.deltaTime;
        if (Input.GetKey("v") == true && counter >= 1f)
        {
            counter = 0;
            switcher += 1;
        }*/

        //if (switcher % 2 == 0)
        //{
            Cursor.visible = false;//隱藏滑鼠
            Cursor.lockState = CursorLockMode.Locked;//把滑鼠鎖定到螢幕中間

            Vector3 camFwd = thirdCamFollow.transform.forward;
            Vector3 camRight = thirdCamFollow.transform.right;

            Vector3 targetLocation = (v * camFwd) / 30;
            targetLocation += (h * camRight) / 30;
            targetLocation.y = 0;

            if (targetLocation.magnitude > 0)
            {
                velo = targetLocation;
            }
            else
            {
                velo = Vector3.zero;
            }
        /*}
        else if (switcher % 2 == 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }*/
    }

    private void Update()
    {
        transform.Translate(velo);
    }

    void Animating(float h, float v){//角色走路動畫呼叫
        bool walking = (h != 0 || v != 0);//是否有走動
        anim.SetBool("isWalking", walking);
    }

    void FixedUpdate(){
        float h = Input.GetAxisRaw("Horizontal");//GetAxisRaw 只會有 1, -1
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
    }
}
