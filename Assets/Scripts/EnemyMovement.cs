using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//為了使用NavMeshAgent

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;

    void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").transform;//取得玩家
        nav = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
            nav.destination = player.position;//敵人追趕玩家
    }
}
