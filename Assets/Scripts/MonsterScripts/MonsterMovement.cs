using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ��κ� ���Ͱ� ��ӹ��� �߻� Ŭ����, �̵� �κ��� ������ �����̴�.
/// </summary>
public abstract class MonsterMovement : MonsterAttack
{
    private bool isAttacking;
    /// <summary>
    /// ����Behavior Start���� �ѹ� ����
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
    /// ���� Behavior Update���� ����
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveUpdate(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        RotateTowardsTarget(player);
        Debug.Log("AttackSpeed ��: " + monsterStatus.runtimeData.AttackSpeed);
    }
    /// <summary>
    /// ���� Behavior OnEnable���� �� �� ����
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveOnEnable(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        //SetAgent(player, monsterStatus, agent, animator);
        StartCoroutine(isAttack(player, monsterStatus, agent, animator));
    }
    /// <summary>
    /// ���� Behavior OnDisable���� �� �� ����
    /// </summary>
    /// <param name="player"></param>
    /// <param name="monsterStatus"></param>
    /// <param name="agent"></param>
    /// <param name="animator"></param>
    public void MoveOnDisable(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        StopCoroutine(isAttack(player, monsterStatus, agent, animator));
    }
    /// <summary>
    /// �÷��̸� �������� NavMeshAgent����
    /// </summary>
    /// <param name="player">�÷��̾�</param>
    /// <param name="monsterStatus">������ ����</param>
    /// <param name="agent">���� Agent</param>
    public void SetAgent(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        Debug.Log("�i�� ����");
        agent.speed = monsterStatus.runtimeData.Speed;
        agent.SetDestination(player.transform.position);
        animator.SetFloat("AttackSpeed", monsterStatus.runtimeData.AttackSpeed);
        animator.SetBool("isWalking", true);
    }
    /// <summary>
    /// �÷��̾ ��� �ٶ�����ϴ� ���͸� �����ų �Լ�, �÷��̾� ������ �ٶ�
    /// </summary>
    /// <param name="player">�÷��̾� ���� ������Ʈ</param>
    public void RotateTowardsTarget(GameObject player)
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized; // �÷��̾��� ���� ���
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // �������θ� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // �ε巴�� ȸ��
    }
    /// <summary>
    /// �÷��̾ ���� ��Ÿ� ���� ������ �i�� ����
    /// </summary>
    /// <param name="agent">���� Agent</param>
    private void StopChase(NavMeshAgent agent, Animator animator)
    {
        animator.SetBool("isWalking", false);
        agent.isStopped = true;
        agent.velocity = new Vector3(0, 0, 0);
    }
    /// <summary>
    /// �ٽ� �÷��̾� �i��, Ȥ�� �а� ������ ������ �����
    /// </summary>
    /// <param name="agent">���� Agent</param>
    private void ResumeChase(NavMeshAgent agent, Animator animator)
    {
        animator.SetBool("isWalking", true);
        agent.isStopped = false;
    }
    /// <summary>
    /// ���� �Լ�, ������ ���������� ��ٷ��� �ϹǷ� �ڷ�ƾ���� �ۼ�
    /// </summary>
    /// <param name="monsterStatus">���� ����</param>
    /// <returns></returns>
    private IEnumerator Attack(MonsterStatus monsterStatus, Animator animator)
    {
        animator.SetTrigger("isAttack");
        yield return new WaitForSecondsRealtime(1 / monsterStatus.runtimeData.AttackSpeed); //�ִϸ��̼� ���ۿ� ���� �ð� ����

        CommonAttack(monsterStatus.runtimeData.Damage, 0.5f, 2f);
    }
    private IEnumerator isAttack(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent, Animator animator)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.02f);
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
    /// <summary>
    /// ��ӹ��� Attack�Լ� ����
    /// </summary>
    /// <param name="dmg">������</param>
    /// <param name="hight">���� ����</param>
    /// <param name="dis">���� ���� ��Ÿ�</param>
    new public void CommonAttack(float dmg, float hight, float dis)
    {
        base.CommonAttack(dmg, hight, dis);
    }
}
