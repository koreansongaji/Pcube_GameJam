using System;
using UnityEngine;
using Weapons;

namespace PlayerScripts
{
    public interface IUpgrades
    {
        public string GetString();
    }
    
    [Serializable]
    public struct UpgradeStat : IUpgrades
    {
        public StatModType modifier;
        public PlayerData.StatType statType;
        public float value;
        
        public string GetString()
        {
            return $"{statType} up : {value} ";
        }
    }
    
    [Serializable]
    public struct GetWeapon : IUpgrades
    {
        public GameObject weaponPrefab;
        public string GetString()
        {
            return $"New Weapon : {weaponPrefab.name}";
        }
    }
    
    [Serializable]
    public struct UpgradeWeapon : IUpgrades
    {
        public WeaponType weaponType;
        public WeaponStatType statType;
        public float value;
        
        
        public string GetString()
        {
            return $"Weapon Upgrade";
        }
    }
}