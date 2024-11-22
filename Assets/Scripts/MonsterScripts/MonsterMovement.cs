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

    /// <summary>
    /// �÷��̸� �������� NavMeshAgent����
    /// </summary>
    /// <param name="player">�÷��̾�</param>
    /// <param name="monsterStatus">������ ����</param>
    /// <param name="agent">���� Agent</param>
    public void SetAgent(GameObject player, MonsterStatus monsterStatus, NavMeshAgent agent)
    {
        Debug.Log("�i�� ����");
        agent.speed = monsterStatus.Speed;
        agent.SetDestination(player.transform.position);
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
    private void StopChase(NavMeshAgent agent)
    {
        //�ִϸ��̼� ����
        agent.isStopped = true;
        agent.velocity = new Vector3(0, 0, 0);
    }
    /// <summary>
    /// �ٽ� �÷��̾� �i��, Ȥ�� �а� ������ ������ �����
    /// </summary>
    /// <param name="agent">���� Agent</param>
    private void ResumeChase(NavMeshAgent agent)
    {
        //�ִϸ��̼� �ٽ� ����
        agent.isStopped = false;
    }
    /// <summary>
    /// ���� �Լ�, ������ ���������� ��ٷ��� �ϹǷ� �ڷ�ƾ���� �ۼ�
    /// </summary>
    /// <param name="monsterStatus">���� ����</param>
    /// <returns></returns>
    private IEnumerator Attack(MonsterStatus monsterStatus)
    {
        //�ִϸ��̼�
        yield return new WaitForSecondsRealtime(1f); //�ִϸ��̼� ���ۿ� ���� �ð� ����

        CommonAttack(monsterStatus.Damage, 0.5f, 2f);
    }
    /// <summary>
    /// ��ӹ��� Attack�Լ� ����
    /// </summary>
    /// <param name="dmg">������</param>
    /// <param name="hight">���� ����</param>
    /// <param name="dis">���� ���� ��Ÿ�</param>
    public void CommonAttack(float dmg, float hight, float dis)
    {
        base.CommonAttack(dmg, hight, dis);
    }
}
