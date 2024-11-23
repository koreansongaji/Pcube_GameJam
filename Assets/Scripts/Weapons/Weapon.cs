using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        [SerializeField] protected WeaponHandler weaponHandler;
        
        public abstract void Setup(WeaponHandler handler);
        public abstract void Attack();
        public abstract bool CanAttack();
        public abstract bool CanUpgrade();
        public abstract void Upgrade();
    }
}