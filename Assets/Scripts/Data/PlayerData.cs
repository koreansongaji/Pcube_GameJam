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
    [SerializeField] public CharacterStat regenHp;
    [SerializeField] public CharacterStat armor;
    [SerializeField] public CharacterStat damage;
    [SerializeField] public CharacterStat projectileSpeed;
    [SerializeField] public CharacterStat duration;
    [SerializeField] public CharacterStat attackRange;
    [SerializeField] public CharacterStat coolTimeReduce;
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
        coolTimeReduce = new CharacterStat(playerData.maxHp.baseValue);
        projectileCount = new CharacterStat(playerData.maxHp.baseValue);
        resurrection = new CharacterStat(playerData.maxHp.baseValue);
        growth = new CharacterStat(playerData.maxHp.baseValue);
    }
}