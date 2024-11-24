using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 대부분 몬스터가 상속받을 추상 클래스, 이동 부분을 구현할 예정이다.
/// </summary>
public abstract class MonsterMovement : MonsterAttack
{
    private bool isAttacking;
    /// <summary>
    /// 몬스터Behavior Start에서 한번 실행
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveStart(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        isAttacking = false;
        SetAgent(player, monsterStatus, agent, animator);
    }
    /// <summary>
    /// 몬스터 Behavior Update에서 실행
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveUpdate(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        RotateTowardsTarget(player);
    }
    /// <summary>
    /// 몬스터 Behavior OnEnable에서 한 번 실행
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveOnEnable(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        //SetAgent(player, monsterStatus, agent, animator);
        StartCoroutine(IsAttack(player, monsterStatus, agent, animator));
    }
    /// <summary>
    /// 몬스터 Behavior OnDisable에서 한 번 실행
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveOnDisable(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        StopCoroutine(IsAttack(player, monsterStatus, agent, animator));
    }
    /// <summary>
    /// 플레이를 목적지로 NavMeshAgent실행
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="monsterStatus">몬스터의 상태</param>
    /// <param name="agent">몬스터 Agent</param>
    public void SetAgent(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        Debug.Log("쫒기 시작");
        agent.speed = monsterStatus.runtimeData.Speed;
        agent.SetDestination(player.transform.position);
        animator.SetFloat("AttackSpeed", monsterStatus.runtimeData.AttackSpeed);
        animator.SetBool("isWalking", true);
    }
    /// <summary>
    /// 플레이어를 계속 바라봐야하는 몬스터만 실행시킬 함수, 플레이어 방향을 바라봄
    /// </summary>
    /// <param name="player">플레이어 게임 오브젝트</param>
    public void RotateTowardsTarget(GameObject player)
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized; // 플레이어의 방향 계산
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // 수평으로만 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 부드럽게 회전
    }
    /// <summary>
    /// 플레이어가 공격 사거리 내에 들어오면 쫒기 멈춤
    /// </summary>
    /// <param name="agent">몬스터 Agent</param>
    public void StopChase(NavMeshAgent agent, Animator animator)
    {
        animator.SetBool("isWalking", false);
        agent.isStopped = true;
        agent.velocity = new Vector3(0, 0, 0);
    }
    /// <summary>
    /// 다시 플레이어 쫒기, 혹시 밀격 공격이 있으면 사용함
    /// </summary>
    /// <param name="agent">몬스터 Agent</param>
    private void ResumeChase(NavMeshAgent agent, Animator animator)
    {
        animator.SetBool("isWalking", true);
        agent.isStopped = false;
    }
    
    /// <summary>
    /// 공격 함수, 공격이 끝날때까지 기다려야 하므로 코루틴으로 작성
    /// </summary>
    /// <param name="monsterStatus">몬스터 상태</param>
    /// <returns></returns>
    private IEnumerator Attack(MonsterStatus monsterStatus, Animator animator)
    {
        animator.SetTrigger("isAttack");
        yield return new WaitForSecondsRealtime(1 / monsterStatus.runtimeData.AttackSpeed); //애니메이션 동작에 따라 시간 조절
        CommonAttack(monsterStatus.runtimeData.Damage, 0.5f, monsterStatus.runtimeData.Range);
    }
    private IEnumerator IsAttack(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            if(monsterStatus.runtimeData.CurHp <= 0)
            {
                StopChase(agent, animator);
            }
            if ((Vector3.Distance(this.gameObject.transform.position, player.transform.position) <= monsterStatus.runtimeData.Range))
            {
                isAttacking = true;
                StopChase(agent, animator);
                yield return StartCoroutine(Attack(monsterStatus, animator));
                isAttacking = false;
            }
            else if ((Vector3.Distance(this.gameObject.transform.position, player.transform.position) > monsterStatus.runtimeData.Range) && !isAttacking)
            {
                ResumeChase(agent, animator);
            }
        }
    }

}
