using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MonsterData를 받아서 몬스터 스탯으로 전환
/// </summary>
public class MonsterStatus : MonoBehaviour
{

    [SerializeField] private MonsterData monsterData;
    public MonsterData Data;
    /// <summary>
    /// 정보 받아오는 함수, Awake에서만 한번 쓰임
    /// </summary>
    private void GetInfo()
    {
        Data = new MonsterData();
        Data.Name = monsterData.Name;
        Data.Speed = monsterData.Speed;
        Data.MaxHp = monsterData.MaxHp;
        Data.CurHp = Data.MaxHp;
        Data.Damage = monsterData.Damage;
        Data.AttackSpeed = monsterData.AttackSpeed;
        Data.Range = monsterData.Range;
    }
    private void Awake()
    {
        GetInfo();
    }

}
