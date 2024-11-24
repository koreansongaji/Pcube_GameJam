using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class StatUpgrader : MonoBehaviour
    {
        [SerializeField] private List<UpgradeStat> upgradeStatDataList = new List<UpgradeStat>();
        
        // 랜덤으로 세 값을 받습니다.
        public List<UpgradeStat> GetRandomUpgradeStats()
        {
            List<UpgradeStat> results = new List<UpgradeStat>();
            
            for (int i = 0; i < 3; i++)
            {
                UpgradeStat randomStat = new UpgradeStat
                {
                    statType = (PlayerData.StatType)UnityEngine.Random.Range
                        (0, Enum.GetValues(typeof(PlayerData.StatType)).Length),
                    value = UnityEngine.Random.Range(1, 11)
                };
                results.Add(randomStat);
            }
            
            return results;
        }
        
        // 업그레이드 스탯을 받아서 적용합니다.
        public void ApplySelectedUpgradeStat(UpgradeStat stat)
        {
            if (! GameManager.Instance.TryGetPlayerObject(out Player p))
            {
                Debug.LogWarning("Player is not found");
                return;
            }
            
            StatModifier modifier = new StatModifier(stat.value, stat.modifier, this);
            p.GetStat().GetStatByType(stat.statType).AddModifier(modifier);
        }
    }
    
    [Serializable]
    public struct UpgradeStat
    {
        public StatModType modifier;
        public PlayerData.StatType statType;
        public float value;
    }
}