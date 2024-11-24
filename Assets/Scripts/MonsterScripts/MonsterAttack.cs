using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    /// <summary>
    /// 몬스터가 공격하는 함수, 래이로 피격 확인
    /// </summary>
    /// <param name="dmg">몬스터의 데미지 확인</param>
    /// <param name="hight">몬스터의 높이 확인(크기가 큰 몬스터는 사용할까봐 넣어둠)</param>
    /// <param name="dis">몬스터의 공격 사거리 확인</param>
    public void CommonAttack(float dmg, float hight, float dis) //몬스터 공격 추상함수
    {
        Debug.Log("CommonAttack 실행");
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, hight, 0), this.transform.forward,
            out hit, dis, LayerMask.GetMask("Player")))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("플레이어 공격");
                hit.collider.GetComponent<Player>().TakeDamage(dmg);
            }
        }
    }
}
