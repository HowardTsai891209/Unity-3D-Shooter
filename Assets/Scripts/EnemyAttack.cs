using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10; //敵人攻擊力
    private bool playerInRange;//玩家是否在攻擊範圍內
    private PlayerHealth playerHealth; //因為要使用PlayerHealth內的 TackDamage
    private float timer;//計時用
    private float timeBetweenAttack = 1f;

    private Animator enemyAnimator;
    private bool playerIsDeath = false;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");//取得Player的tag
        playerHealth = player.GetComponent<PlayerHealth>();//連接上PlayerHealth(為了扣玩家血量)
        enemyAnimator = GetComponent<Animator>();//取得enemy的Animator
    }

    public void playerDeathAction()
    {
        playerIsDeath = true;//玩家死亡
        enemyAnimator.SetTrigger("PlayerDead");//敵人要進入idle狀態的條件 on
        GetComponent<EnemyMovement>().enabled = false;//敵人停止移動
        GetComponent<NavMeshAgent>().enabled = false;//敵人AI停止
    }

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathEvent += playerDeathAction;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeathEvent -= playerDeathAction;
    }

    //用Enemy的sphere collider當作參考
    private void OnTriggerEnter(Collider other)//如果碰撞到 
    {
        if(other.tag == playerHealth.tag)//且tag是玩家
        {
            playerInRange = true;//在範圍內
        }
    }

    private void OnTriggerExit(Collider other)//如果離開碰撞
    {
        if(other.tag == playerHealth.tag)//且tag是玩家
        {
            playerInRange = false;//不在範圍內
        }
    }

    private void Attack()
    {
        timer = 0;
        playerHealth.TakeDamage(attackDamage);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (playerInRange && timer >= timeBetweenAttack && playerIsDeath == false)
        {
            Attack();
        }
    }
}
