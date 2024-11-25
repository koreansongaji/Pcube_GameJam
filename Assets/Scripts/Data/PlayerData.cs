using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            UnityEditor.EditorUtility.SetDirty(this); // 변경사항 무효화
        }
    }
#endif
    
    [SerializeField] public CharacterStat maxHp;
    [SerializeField] public CharacterStat currentHp;
    [SerializeField] public CharacterStat hpRegen;
    [SerializeField] public CharacterStat armor;
    [SerializeField] public CharacterStat damage;
    [SerializeField] public CharacterStat projectileSpeed;
    [SerializeField] public CharacterStat duration;
    [SerializeField] public CharacterStat attackRange;
    [SerializeField] public CharacterStat coolTimeReduce;
    [SerializeField] public CharacterStat projectileCount;
    [SerializeField] public CharacterStat resurrection;
    [SerializeField] public CharacterStat growth;
    
    public CharacterStat GetStatByType(StatType statType)
    {
        return statType switch
        {
            StatType.MAX_HP => maxHp,
            StatType.CURRENT_HP => currentHp,
            StatType.REGEN_HP => hpRegen,
            StatType.ARMOR => armor,
            StatType.DAMAGE => damage,
            StatType.PROJECTILE_SPEED => projectileSpeed,
            StatType.DURATION => duration,
            StatType.ATTACK_RANGE => attackRange,
            StatType.COOL_TIME_REDUCE => coolTimeReduce,
            StatType.PROJECTILE_COUNT => projectileCount,
            StatType.RESURRECTION => resurrection,
            StatType.GROWTH => growth,
            _ => null
        };
    }

    public enum StatType
    {
        MAX_HP,
        CURRENT_HP,
        REGEN_HP,
        ARMOR,
        DAMAGE,
        PROJECTILE_SPEED,
        DURATION,
        ATTACK_RANGE,
        COOL_TIME_REDUCE,
        PROJECTILE_COUNT,
        RESURRECTION,
        GROWTH
    }
}