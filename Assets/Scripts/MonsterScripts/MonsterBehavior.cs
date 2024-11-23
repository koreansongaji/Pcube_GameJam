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
    private Animator animator;

    /// <summary>
    /// ���� �޾ƿ��� �Լ�, Awake�� ó�� �� �� ���δ�.
    /// </summary>
    private void GetInfo()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        monsterStatus = this.GetComponent<MonsterStatus>();
        animator = this.GetComponent<Animator>();
    }
    private void Awake()
    {
        GetInfo();
    }
    private void OnEnable()
    {
        MoveOnEnable(player, monsterStatus, agent, animator);
    }
    void Start()
    {
        MoveStart(player, monsterStatus, agent, animator);
    }
    
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * 5, Color.red);
    }
    private void FixedUpdate()
    {
        MoveUpdate(player, monsterStatus, agent, animator);
    }
    private void OnDisable()
    {
        MoveOnDisable(player, monsterStatus, agent, animator);    
    }

    public void TakeDamage(float damage)
    {
        monsterStatus.Data.CurHp -= damage;
    }
}
