using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    // 최대 체력, 초당 재생, 방어력, 피해량, 투사체 속도, 지속시간, 공격범위, 쿨타임, 투사체 수, 부활, 성장
    [SerializeField] public CharacterStat maxHp;
    [SerializeField] public CharacterStat regenHp;
    [SerializeField] public CharacterStat armor;
    [SerializeField] public CharacterStat damage;
    [SerializeField] public CharacterStat projectileSpeed;
    [SerializeField] public CharacterStat duration;
    [SerializeField] public CharacterStat attackRange;
    [SerializeField] public CharacterStat coolTime;
    [SerializeField] public CharacterStat projectileCount;
    [SerializeField] public CharacterStat resurrection;
    [SerializeField] public CharacterStat growth;
}