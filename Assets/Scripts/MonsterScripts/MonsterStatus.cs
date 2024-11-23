using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MonsterData�� �޾Ƽ� ���� �������� ��ȯ
/// </summary>
public class MonsterStatus : MonoBehaviour
{

    [SerializeField] private MonsterData monsterData;
    public MonsterData Data;
    /// <summary>
    /// ���� �޾ƿ��� �Լ�, Awake������ �ѹ� ����
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
