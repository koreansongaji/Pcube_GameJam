using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    /// <summary>
    /// ���Ͱ� �����ϴ� �Լ�, ���̷� �ǰ� Ȯ��
    /// </summary>
    /// <param name="dmg">������ ������ Ȯ��</param>
    /// <param name="height">������ ���� Ȯ��(ũ�Ⱑ ū ���ʹ� ����ұ�� �־��)</param>
    /// <param name="dis">������ ���� ��Ÿ� Ȯ��</param>
    public void CommonAttack(float dmg, float height, float dis) //���� ���� �߻��Լ�
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, height, 0), this.transform.forward,
            out hit, dis, LayerMask.GetMask("Player")))
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<Player>().TakeDamage(dmg);
            }
        }
    }
}
