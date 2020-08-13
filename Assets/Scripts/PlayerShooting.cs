using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float range = 100f;
    private Ray shootRay;
    private RaycastHit shootHit;//目標判定物
    private int shootableMask;//要射到的目標 layer
    private int environmentMask;//要射到的目標 layer
    private Light gunLight;//開火 火光
    private ParticleSystem gunParticle;//開火 火花
    private AudioSource gunAudio;//槍聲
    private LineRenderer gunLine;//子彈軌跡
    public float timeBetweenBullets = 0.15f;//每次發射的間距
    private float effectsDisplaytime = 0.2f;//開槍特效持續時間
    float timer;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Enemy");
        environmentMask = LayerMask.GetMask("environment");
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Shoot(){
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticle.Stop();
        gunParticle.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);//element 0 = 自己的位置 element 1 = 敵人的位置
        shootRay.origin = transform.position; //shootRay原點
        shootRay.direction = transform.forward; //shootRay方向 transform=自身座標(才會跟著轉) Vector3=世界座標

        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask)){//Raycast(原點,終點,距離,layer)
            gunLine.SetPosition(1, shootHit.point);//打到敵人不會穿過
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();//取得打到的敵人
            enemyHealth.takeDamage(damagePerShot, shootHit.point);//呼叫takeDamage並丟入參數(攻擊力, 擊中的位置)
        }
        else if(Physics.Raycast(shootRay, out shootHit, range, environmentMask)){
            gunLine.SetPosition(1, shootHit.point);//打到環境不會穿過
        }else{
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    void DisableEffects(){//特效隱蔽 (不讓特效停在半空中)
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets){
            Shoot();
        }
        if(timer >= timeBetweenBullets * effectsDisplaytime){//開槍間隔越長 特效持續時間越長
            DisableEffects();
        }
    }
}
