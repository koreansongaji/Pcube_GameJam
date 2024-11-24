using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerScripts
{
    public class StatUpgrader : MonoBehaviour
    {
        [SerializeField] private List<UpgradeStat> upgradeStatDataList = new List<UpgradeStat>();
        
        // �������� �� ���� �޽��ϴ�.
        public List<UpgradeStat> GetRandomUpgradeStats()
        {
            List<UpgradeStat> results = new List<UpgradeStat>();
            
            for (int i = 0; i < 3; i++)
            {
                // random Select Stat from UpgradeStatDataList
                UpgradeStat randomStat = new UpgradeStat();
                
                int randomIndex = UnityEngine.Random.Range(0, upgradeStatDataList.Count);
                
                randomStat = upgradeStatDataList[randomIndex];
                results.Add(randomStat);
            }
            
            return results;
        }
        
        // ���׷��̵� ������ �޾Ƽ� �����մϴ�.
        public void ApplySelectedUpgradeStat(UpgradeStat stat)
        {
            if (!GameManager.Instance.TryGetPlayerObject(out Player p))
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