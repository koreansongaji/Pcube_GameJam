using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;

namespace PlayerScripts
{
    public class StatUpgrader : MonoBehaviour
    {
        [SerializeField] private List<UpgradeStat> upgradeStatDataList = new List<UpgradeStat>();
        [SerializeField] private List<GetWeapon> weaponDataList = new List<GetWeapon>();

        private void Awake()
        {
            foreach (GetWeapon a in weaponDataList.Where(a => a.weaponPrefab == null))
            {
                Debug.LogError("Weapon prefab is null");
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
        
        // 랜덤으로 세 값을 받습니다.
        public List<IUpgrades> GetRandomUpgradeStats()
        {
            List<IUpgrades> results = new List<IUpgrades>();
            
            List<int> randomNumbers = 
                Helpers.RandomNumberGenerator.GetRandomNumbers(
                    3, 
                    0, 
                    upgradeStatDataList.Count + weaponDataList.Count
                    );
            
            foreach (int number in randomNumbers)
            {
                if (number >= upgradeStatDataList.Count)
                {
                    results.Add(weaponDataList[number - upgradeStatDataList.Count]);
                    continue;
                }
                
                results.Add(upgradeStatDataList[number]);
            }
            
            return results;
        }
        
        // 업그레이드 스탯을 받아서 적용합니다.
        private void ApplySelectedUpgradeStat(UpgradeStat upgradeInfo)
        {
            if (!GameManager.Instance.TryGetPlayerObject(out Player p))
            {
                Debug.LogWarning("Player is not found");
                return;
            }
            
            StatModifier modifier = new StatModifier(upgradeInfo.value, upgradeInfo.modifier, this);

            float beforeValue = p.GetStat().GetStatByType(upgradeInfo.statType).Value;
            
            p.GetStat().GetStatByType(upgradeInfo.statType).AddModifier(modifier);
            
            float afterValue = p.GetStat().GetStatByType(upgradeInfo.statType).Value;
            
            if (upgradeInfo.statType == PlayerData.StatType.MAX_HP)
            {
                Debug.Log($"HP up : {beforeValue} -> {afterValue}");
                p.Heal(afterValue - beforeValue);
            }
        }
        
        public void ApplyUpgrade(IUpgrades upgrade)
        {
            if(upgrade is UpgradeStat stat)
            {
                ApplySelectedUpgradeStat(stat);
            }
            
            if(upgrade is GetWeapon weapon)
            {
                ApplySelectedWeapon(weapon);
            }
        }

        private void ApplySelectedWeapon(GetWeapon weapon)
        {
            // 1, try get player object
            if (!GameManager.Instance.TryGetPlayerObject(out Player p))
            {
                Debug.LogWarning("Player is not found");
                return;
            }
            
            // 2, get player weapon handler
            WeaponHandler weaponHandler = p.GetComponent<WeaponHandler>();
            
            GameObject weaponPrefab = weapon.weaponPrefab;
            GameObject weaponObject = Instantiate(weaponPrefab, weaponHandler.transform, true);
            weaponObject.TryGetComponent(out Weapon weaponComponent);
            
            weaponHandler.AddWeapon(weaponComponent);
            
            // 3. remove from list
            weaponDataList.Remove(weapon);
        }
    }
    
    
}