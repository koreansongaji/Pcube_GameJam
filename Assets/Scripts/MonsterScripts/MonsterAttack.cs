using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    /// <summary>
    /// ���Ͱ� �����ϴ� �Լ�, ���̷� �ǰ� Ȯ��
    /// </summary>
    /// <param name="dmg">������ ������ Ȯ��</param>
    /// <param name="hight">������ ���� Ȯ��(ũ�Ⱑ ū ���ʹ� ����ұ�� �־��)</param>
    /// <param name="dis">������ ���� ��Ÿ� Ȯ��</param>
    public void CommonAttack(float dmg, float hight, float dis) //���� ���� �߻��Լ�
    {
        Debug.Log("CommonAttack ����");
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, hight, 0), this.transform.forward,
            out hit, dis, LayerMask.GetMask("Player")))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("�÷��̾� ����");
                hit.collider.GetComponent<Player>().TakeDamage(dmg);
            }
        }
    }
}
