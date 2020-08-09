using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public  Transform target;
    public float smoothing = 5f;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;//兩個相減得到向量
    }

    void FixedUpdate()
    {
        Vector3 targerCameraPosition = target.position + offset;//將camera的位置加上向量 兩者會維持一樣的距離
        transform.position = Vector3.Lerp(transform.position,
        targerCameraPosition,
        Time.deltaTime * smoothing);//Lerp(起始點,終點,在多久時間內完成)
    }
}
