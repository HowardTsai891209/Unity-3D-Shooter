using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontSight : MonoBehaviour
{
    private int switcher = 0;
    private float counter; 
    public Image frontSight;
    public Color ThirdColor = new Color(255f, 0f, 0f, 255f);

    private void Update()
    {
        counter += Time.deltaTime;
        if (Input.GetKey("v") == true && counter >= 1f)
        {
            counter = 0;
            switcher += 1;
        }
        if (switcher % 2 == 0)
        {
            frontSight.color = ThirdColor;
        }
        else if (switcher % 2 == 1)
        {
            frontSight.color = Color.clear;
        }
    }
}
