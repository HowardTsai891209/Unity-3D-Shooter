using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;//計算分數
    public static Text scoreText;//顯示文字
    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }
    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
