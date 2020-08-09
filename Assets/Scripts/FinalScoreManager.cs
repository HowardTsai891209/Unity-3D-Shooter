using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class FinalScoreManager : MonoBehaviour
{
    public static int finalScore;//計算分數
    private Text finalScoreText;//顯示文字
    public Color flashColor = new Color(0f, 255f, 0f, 255f);
    public GameObject countDownObj;
    private int minute;
    private int second;

    private EnemyManager enemyManager;
    private EnemyMovement enemyMovement;
    private NavMeshAgent enemyNav;
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        finalScoreText = GetComponent<Text>();
        countDownObj = GameObject.FindGameObjectWithTag("CountDownTag");
        minute = countDownObj.GetComponent<CountDownManager>().time_minute;
        second = countDownObj.GetComponent<CountDownManager>().time_second;

        GameObject enemyManagerObj = GameObject.FindGameObjectWithTag("enemyManagerTag");
        enemyManager = enemyManagerObj.GetComponent<EnemyManager>();
    }
    private void Start()
    {
        InvokeRepeating("timer", 0, 1);
    }

    private void Update()
    {
        GameObject enemyObj = GameObject.FindGameObjectWithTag("enemyTag");
        //enemyMovement = enemyObj.GetComponent<EnemyMovement>();
        //enemyNav = enemyObj.GetComponent<NavMeshAgent>();
        //enemyAttack = enemyObj.GetComponent<EnemyAttack>();
    }

    public void timer()
    {
        second -= 1;
        if (second < 10)
        {
            if (second < 0)
            {
                second = 59;
                minute -= 1;
            }
        }
        if (minute <= 0 && second <= 0)
        {           
            PopScore();                                  
        }
    }

    /*private void stopEnemy()
    {
        enemyMovement.enabled = false;
        enemyNav.enabled = false;
        enemyAttack.enabled = false;
    }*/
    public void PopScore()
    {
        CountDownManager.timeText.color = Color.clear;
        ScoreManager.scoreText.color = Color.clear;
        finalScoreText.color = flashColor;
        finalScoreText.text = "Score: " + finalScore;

        CancelInvoke("timer");
        enemyManager.stopSpawn();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyTag");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
    }
}
