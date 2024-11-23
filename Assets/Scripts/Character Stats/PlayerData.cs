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
    
    //복사 생성자
    public PlayerData(PlayerData playerData)
    {
        maxHp = new CharacterStat(playerData.maxHp.baseValue);
        currentHp = new CharacterStat(playerData.maxHp.baseValue);
        regenHp = new CharacterStat(playerData.maxHp.baseValue);
        armor = new CharacterStat(playerData.maxHp.baseValue);
        damage = new CharacterStat(playerData.maxHp.baseValue);
        projectileSpeed = new CharacterStat(playerData.maxHp.baseValue);
        duration = new CharacterStat(playerData.maxHp.baseValue);
        attackRange = new CharacterStat(playerData.maxHp.baseValue);
        coolTime = new CharacterStat(playerData.maxHp.baseValue);
        projectileCount = new CharacterStat(playerData.maxHp.baseValue);
        resurrection = new CharacterStat(playerData.maxHp.baseValue);
        growth = new CharacterStat(playerData.maxHp.baseValue);
    }
}