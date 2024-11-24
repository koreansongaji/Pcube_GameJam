using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace PlayerScripts
{
    public class StatUpgrader : MonoBehaviour
    {
        [SerializeField] private List<UpgradeStat> upgradeStatDataList = new List<UpgradeStat>();
        [SerializeField] private List<GetWeapon> weaponDataList = new List<GetWeapon>();
        
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
        private void ApplySelectedUpgradeStat(UpgradeStat stat)
        {
            if (!GameManager.Instance.TryGetPlayerObject(out Player p))
            {
                Debug.LogWarning("Player is not found");
                return;
            }
            
            StatModifier modifier = new StatModifier(stat.value, stat.modifier, this);
            p.GetStat().GetStatByType(stat.statType).AddModifier(modifier);
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
            GameObject weaponObject = Instantiate(weaponPrefab);
            weaponObject.transform.SetParent(weaponHandler.transform);
            weaponObject.TryGetComponent(out Weapon weaponComponent);
            
            weaponHandler.AddWeapon(weaponComponent);
            
            // 3. remove from list
            weaponDataList.Remove(weapon);
        }
    }
    
    
}