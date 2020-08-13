using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startHelth = 100;
    private int currentHealth;//敵人目前血量
    private Animator anim;//敵人動作
    private bool isDead;//敵人是否死亡
    private bool isSinking;//敵人死亡後下沉

    public AudioClip deadClip;//先把死亡音效放到Enemy內 以便切換
    private AudioSource enemyAudio;//敵人身上所有音效 在這裡切換
    private ParticleSystem hiyParticle;//敵人被打到的粒子特效
    public int enemyAliveNumber;
    private FinalScoreManager finalScoreManager;

    public int score = 10;//敵人的分數
    private void Awake()
    {
        anim = GetComponent<Animator>();//取得敵人的Animator
        currentHealth = startHelth;//設置敵人血量
        enemyAudio = GetComponent<AudioSource>();//取得敵人的AudioSource
        hiyParticle = GetComponentInChildren<ParticleSystem>();//取得敵人的ParticleSystem 

        GameObject finalScoreManagerObj = GameObject.FindGameObjectWithTag("FianlScoreTag");
        finalScoreManager = finalScoreManagerObj.GetComponent<FinalScoreManager>();

        GameObject enemyHealthObj = GameObject.FindGameObjectWithTag("enemyManagerTag");
        //enemyAliveNumber = enemyHealthObj.GetComponent<EnemyManager>().enemyNumber + 1;
    }


    public void Death()//敵人死亡function
    {
        isDead = true;//敵人死亡
        enemyAudio.clip = deadClip;//將enemyAudio切換成deadClip
        enemyAudio.Play();//播放deadClip
        anim.SetTrigger("isDead");//敵人死亡動畫trigger on
        GetComponent<EnemyMovement>().enabled = false;//以下 關閉敵人的動作
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        ScoreManager.score += score;
        FinalScoreManager.finalScore += score;
        //enemyAliveNumber -= 1;     
    }

    public void takeDamage(int amount, Vector3 postion)//敵人受到傷害
    {
        if (isDead) return;
        enemyAudio.Play();//敵人受傷音效
        hiyParticle.transform.position = postion;//將打擊特效的位置 設定成擊中的位置
        hiyParticle.Play();//播放擊中特效
        currentHealth -= amount;//敵人扣血
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void StartSinking()//屍體下沉
    {
        isSinking = true;
        Destroy(gameObject, 0.5f); //Destroy(破壞物體, 多久時間完成)
    }
    private void Update()
    {
        //Debug.Log(enemyAliveNumber);
        if (FinalScoreManager.finalScore >= 200)
        //if(enemyAliveNumber == 0)
        {
            finalScoreManager.PopScore();
        }
        if (this.isSinking)
        {
            transform.Translate(Vector3.down * Time.deltaTime);//Translate讓物體移動 單純移動無操作可放Update
        }
    }

}
