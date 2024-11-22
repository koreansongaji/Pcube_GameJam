using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// ������ ��ü���� �ൿ�� ���ϴ� ��ũ��Ʈ
/// </summary>
public class MonsterBehavior : MonsterMovement
{
    private NavMeshAgent agent;
    private MonsterStatus monsterStatus;
    private GameObject player;

    /// <summary>
    /// ���� �޾ƿ��� �Լ�, Awake�� ó�� �� �� ���δ�.
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
