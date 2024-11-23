using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Runtime Data. Do not modify")]
        [SerializeField] protected bool atkTrigger;
        [SerializeField] protected WeaponHandler weaponHandler;
        
        [Header("Initial Data")]
        [SerializeField] protected WeaponData weaponData;
        
        
        
        
        public abstract void Setup(WeaponHandler handler);
        public abstract void Attack();

        public virtual bool CanAttack()
        {
            return atkTrigger;
        }
        
        public abstract bool CanUpgrade();
        public abstract void Upgrade();

        protected abstract float CalculateFinalDamage();

        protected IEnumerator Cooldown(float time)
        {
            yield return new WaitForSeconds(time);
            atkTrigger = true;
        }
    }
}