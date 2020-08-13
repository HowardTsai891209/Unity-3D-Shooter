    using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemy;
    //public GameObject enemy;
    public int enemyNumber;
    public float delayTime = 1f;//第一次執行 等待時間
    public float repeatTime = 3f;//每多久 執行一次
    public Transform[] spawnPoints;//重生點陣列
    private bool playerIsDead;//玩家是否死亡

    private void playerDeathAction()
    {
        playerIsDead = true;//玩家死亡
    }
    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += playerDeathAction;
    }
    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= playerDeathAction;
    }
    private void Spawn()
    {
        enemyNumber -= 1;
        if (playerIsDead || enemyNumber <= 0)//如果玩家死亡
        {
            stopSpawn();
        }
        int pointIndex = Random.Range(0, spawnPoints.Length);//產生亂數 0~spawnPoints陣列長度
        int rand = Random.Range(0, enemy.Length);

        Instantiate(enemy[rand],
            spawnPoints[pointIndex].position,
            spawnPoints[pointIndex].rotation);//Instantiate(生成的東西,生成位置,生成時面對的方向)      
    }

    public void stopSpawn()
    {
        CancelInvoke("Spawn");//停止生怪
        return;
    }

    void Start()
    {
        InvokeRepeating("Spawn", delayTime, repeatTime);//重複呼叫(含式, 第一次呼叫等待時間, 每多久呼叫一次)      
    }
}
