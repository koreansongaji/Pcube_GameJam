using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        public abstract void Setup();
        public abstract void Attack();
        public abstract bool CanAttack();
        public abstract bool CanUpgrade();
        public abstract void Upgrade();
        
        
    }
}