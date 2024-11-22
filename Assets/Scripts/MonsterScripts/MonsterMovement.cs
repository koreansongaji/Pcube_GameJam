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

    /// <summary>
    /// 플레이를 목적지로 NavMeshAgent실행
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="monsterStatus">몬스터의 상태</param>
    /// <param name="agent">몬스터 Agent</param>
    public void SetAgent(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent)
    {
        Debug.Log("쫒기 시작");
        agent.speed = monsterStatus.Speed;
        agent.SetDestination(player.transform.position);
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
    private void StopChase(NavMeshAgent agent)
    {
        //애니메이션 중지
        agent.isStopped = true;
        agent.velocity = new Vector3(0, 0, 0);
    }
    /// <summary>
    /// 다시 플레이어 쫒기, 혹시 밀격 공격이 있으면 사용함
    /// </summary>
    /// <param name="agent">몬스터 Agent</param>
    private void ResumeChase(NavMeshAgent agent)
    {
        //애니매이션 다시 시작
        agent.isStopped = false;
    }
    /// <summary>
    /// 공격 함수, 공격이 끝날때까지 기다려야 하므로 코루틴으로 작성
    /// </summary>
    /// <param name="monsterStatus">몬스터 상태</param>
    /// <returns></returns>
    private IEnumerator Attack(MonsterStatus monsterStatus)
    {
        //애니매이션
        yield return new WaitForSecondsRealtime(1f); //애니메이션 동작에 따라 시간 조절

        CommonAttack(monsterStatus.Damage, 0.5f, 2f);
    }
    /// <summary>
    /// 상속받은 Attack함수 실행
    /// </summary>
    /// <param name="dmg">데미지</param>
    /// <param name="hight">몬스터 높이</param>
    /// <param name="dis">몬스터 공격 사거리</param>
    public void CommonAttack(float dmg, float hight, float dis)
    {
        base.CommonAttack(dmg, hight, dis);
    }
}
