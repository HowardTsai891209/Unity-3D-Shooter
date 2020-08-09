using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    public int time_minute = 3;
    public int time_second = 0;
    public static Text timeText;//顯示文字

    private void Awake()
    {
        timeText = GetComponent<Text>();
    }
    private void Start()
    {
        InvokeRepeating("timer", 0, 1);
    }

    public void timer()
    {      
        time_second -= 1;    
        if(time_second < 10)
        {
            timeText.text = "Time: " + time_minute + ":0" + time_second;
            if (time_second < 0)
            {
                time_second = 59;
                time_minute -= 1;
                timeText.text = "Time: " + time_minute + ":" + time_second;
            }            
        }else if(time_second >= 10)
        {
            timeText.text = "Time: " + time_minute + ":" + time_second;
        }

        if (time_minute <= 0 && time_second <= 0)
        {
            CancelInvoke("timer");
        }
    }
}

