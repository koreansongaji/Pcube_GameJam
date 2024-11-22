using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField] public CharacterStat maxHp;
    [SerializeField] public CharacterStat currentHp;
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