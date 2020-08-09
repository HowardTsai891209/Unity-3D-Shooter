using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;//初始血量 拷貝用 不可更改
    public Slider healthSlider;//血條 顯示用
    private static int currentHealth;//目前血量 計算用

    public AudioClip deathClip; //先把死亡音效放到Player內 以便切換
    public Image damageImage;   //受傷閃爍
    public float flashSpeed = 5f;//閃爍頻率
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);//閃爍顏色(R,G,B,A) A:透明度
    private bool damaged = false;//是否受到傷害
    private AudioSource playerAudio;//玩家身上所有音效 在這裡切換

    private bool isDeath = false;//玩家是否死亡
    private Animator deathAnimator;//玩家死亡動畫

    public delegate void PlayerDeathAction();//delegate幫忙做玩家死亡後的指令
    public static event PlayerDeathAction PlayerDeathEvent;

    private void Awake()
    {
        healthSlider.maxValue = startingHealth;
        currentHealth = startingHealth;
        playerAudio = GetComponent<AudioSource>();//取得Player中的AudioSource
        deathAnimator = GetComponent<Animator>();//取得Player中的Animator
        if (currentHealth <= 0){
            healthSlider.value = startingHealth;
            currentHealth = startingHealth;
        }else {
            healthSlider.value = startingHealth;
        }
    }

    public void TakeDamage(int amount){//受傷害
        damaged = true;
        if (isDeath) return;
        playerAudio.Play();//播放受傷音效
        currentHealth -= amount;//血量減掉敵人攻擊力
        healthSlider.value = currentHealth;
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDeath = true;
        playerAudio.clip = deathClip;//將AudioSource的clip切換成deathClip
        playerAudio.Play();//播放音效
        deathAnimator.SetTrigger("Die");//玩家死亡動作
        GetComponent<PlayerMovement>().enabled = false;//玩家無法移動
        GetComponentInChildren<PlayerShooting>().enabled = false;//玩家無法射擊

        if (PlayerDeathEvent != null)
        {
            PlayerDeathEvent();
        }
    }

    private void Update()
    {
        if (damaged)//如果受到傷害
        {         
            damageImage.color = flashColor;//把畫面閃爍成紅色
            damaged = false;//馬上把受傷開關 關閉 否則會不斷閃爍
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime * flashSpeed);
            //把畫面變回正常 Lerp(原本的顏色,要變的顏色(clear:全部清除),多少時間內)
        }
    }

}
