using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 몬스터의 전체적인 행동을 정하는 스크립트
/// </summary>
public class MonsterBehavior : MonsterMovement
{
    private NavMeshAgent _agent;
    private MonsterStatus _monsterStatus;
    private GameObject _player;
    private Animator _animator;

    private PoolingHandler _poolingHandler;
    private int _monsterKind;
    private static readonly int IS_DEATH = Animator.StringToHash("isDeath");

    private bool _dead;

    /// <summary>
    /// 정보 받아오는 함수, Awake에 처음 한 번 쓰인다.
    /// </summary>
    private void GetInfo()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _monsterStatus = GetComponent<MonsterStatus>();
        _animator = GetComponent<Animator>();
        _poolingHandler = FindObjectOfType<PoolingHandler>();
    }
    
    private void Awake()
    {
        GetInfo();
    }

    private void OnEnable()
    {
        MoveOnEnable(_player, _monsterStatus, _agent, _animator);
    }

    private void Start()
    {
        MoveStart(_player, _monsterStatus, _agent, _animator);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * 5, Color.red);
    }
    private void FixedUpdate()
    {
        MoveUpdate(_player, _monsterStatus, _agent, _animator);
        CheckDeath();
    }
    private void OnDisable()
    {
        MoveOnDisable(_player, _monsterStatus, _agent, _animator);    
    }

    public void TakeDamage(float damage)
    {
        _monsterStatus.runtimeData.CurHp -= damage;
        UIManager.Instance.DamageFloat(Camera.main.WorldToScreenPoint(transform.position), damage);
    }

    private void CheckDeath()
    {
        if(_dead) return;
        
        if(_monsterStatus.runtimeData.CurHp <= 0)
        {
            _dead = true;
            
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        StopChase(_agent, _animator);
        _animator.SetBool(IS_DEATH, true);
        ExpPoolSystem.Instance.CreateExpSphere(this.transform.position);
        yield return new WaitForSecondsRealtime(0.6f);
        _poolingHandler.DeActiveMonster[_monsterStatus.runtimeData.Kind].   Add(gameObject);
        _poolingHandler.ActiveMonster  [_monsterStatus.runtimeData.Kind].Remove(gameObject);
        gameObject.SetActive(false);
    }
}
