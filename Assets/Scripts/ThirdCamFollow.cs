using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamFollow : MonoBehaviour
{
    public GameObject thirdPersonPlayer;    //角色
    public GameObject FollowCamera;         //跟隨相機
    public float CameraSmoothTime = 0;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    /*private void Start()
    {
        offset = transform.position - thirdPersonPlayer.transform.position;//兩個相減得到向量
    }*/

    void Update()
    {
        //相機跟隨旋轉
        float x = 5 * Input.GetAxis("Mouse X");
        //以下為相機與角色同步旋轉是
        FollowCamera.transform.rotation = Quaternion.Euler(
            FollowCamera.transform.rotation.eulerAngles +
            Quaternion.AngleAxis(x, Vector3.up).eulerAngles
        );//原理： 物體當前的尤拉角 + 滑鼠x軸上的增量所產生的夾角

        thirdPersonPlayer.transform.rotation = Quaternion.Euler(
            thirdPersonPlayer.transform.rotation.eulerAngles +
            Quaternion.AngleAxis(x, Vector3.up).eulerAngles
        );//同理
          //------------------------------------------------------>>>>>>>>
          //相機跟隨移動
        Vector3 TargetCameraPosition = thirdPersonPlayer.transform.TransformPoint(new Vector3(0, 2f, -4f));//獲取相機跟隨的相對位置，再轉為世界座標

        FollowCamera.transform.position = Vector3.SmoothDamp(
            FollowCamera.transform.position,
            TargetCameraPosition,
            ref velocity,
            CameraSmoothTime, //最好為0
            Mathf.Infinity,
            Time.deltaTime
        );
    }
    /*private void FixedUpdate()
    {
        transform.position = thirdPersonPlayer.transform.position + offset;//將camera的位置加上向量 兩者會維持一樣的距離
    }*/
}
