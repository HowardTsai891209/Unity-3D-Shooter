using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject SkyCam, ThirdCam; //兩個不同的攝影機
    public int switcher = 0;
    private float counter;

    private void Awake()
    {
        SkyCam.SetActive(false);
        ThirdCam.SetActive(true);
    }
    private void Start()
    {
        //取得第三人稱攝影機最一開始的transform(position, rotation不確定要不要)
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (Input.GetKey("v") == true && counter >= 1f)
        {
            counter = 0;
            switcher += 1;          
        }

        if (switcher % 2 == 0)//Third
        {
            do
            {
                //因為從SkyCam切回來時 第三人稱攝影機位置會跑掉
                //所以要把第三人稱攝影機位置的位置指定為一開始指定的初始位置
                //do while在這裡面應該只會執行一次
            } while (switcher % 2 != 0);
            SkyCam.SetActive(false);
            ThirdCam.SetActive(true);           
            GameObject.Find("ThirdCamManager").GetComponent<ThirdCamFollow>().enabled = true;
        }
        else if (switcher % 2 == 1)//SkyCam
        {
            SkyCam.SetActive(true);
            ThirdCam.SetActive(false);
            GameObject.Find("ThirdCamManager").GetComponent<ThirdCamFollow>().enabled =false;
        }
    }
}
