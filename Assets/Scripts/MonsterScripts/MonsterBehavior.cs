using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 몬스터의 전체적인 행동을 정하는 스크립트
/// </summary>
public class MonsterBehavior : MonsterMovement
{
    private NavMeshAgent agent;
    private MonsterStatus monsterStatus;
    private GameObject player;

    /// <summary>
    /// 정보 받아오는 함수, Awake에 처음 한 번 쓰인다.
    /// </summary>
    private void GetInfo()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        monsterStatus = this.GetComponent<MonsterStatus>();
    }
    private void Awake()
    {
        GetInfo();
    }
    void Start()
    {
        SetAgent(player, monsterStatus, agent);
    }

    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * 5, Color.red);
        RotateTowardsTarget(player);
    }
}
