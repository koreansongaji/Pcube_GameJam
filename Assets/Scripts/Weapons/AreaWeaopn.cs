using Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.WeaponSubScripts;

namespace Weapons
{
    public class AreaWeapon : Weapon
    {
         private Player _player;
        [SerializeField] private GameObject areaPrefab;
        [SerializeField] private AimType currentType;
        
        private GameObjectPool areaPool;
        
        public override void Setup(WeaponHandler handler)
        {
            weaponHandler = handler;
            _player = handler.GetComponent<Player>();
            areaPool = new GameObjectPool(areaPrefab, handler.transform, 10);
            atkTrigger = true;
        }
        
        public override void Attack()
        {
            if(TryGetTarget(out Vector3 target) == false)
                return;
            
            // shoot LeavesBullet
            GameObject area = areaPool.Get();
            area.TryGetComponent(out SingleArea areaScript);
            
            area.transform.position = weaponHandler.transform.position;
            areaScript.SetArgs(
                CalculateFinalDamage(),
                CalculateAreaSize(),
                CalculateDuration(), 
                areaPool
                );
        }

        protected override float CalculateFinalDamage()
        {
            float percentage = _player.GetStat().damage.Value;
            
            float damage = weaponData.damage * ((100 + percentage) * 0.01f);
            return damage;
        }

        private float CalculateAreaSize()
        {
            return weaponData.attackRange * 
                   (100 + _player.GetStat().attackRange.Value) * 0.01f;
        }

        private float CalculateDuration()
        {
            return weaponData.duration * 
                   (100 + _player.GetStat().duration.Value) * 0.01f;
        }
        
        
        private float CalculateCooldown()
        {
            return weaponData.coolTime *
                   (100 - _player.GetStat().coolTimeReduce.Value) * 0.01f;
        }

        private bool TryGetTarget(out Vector3 target)
        {
            if (currentType == AimType.TARGET)
            {
                MonsterBehavior monster = 
                    NearestMonsterFinder.FindNearestMonster(weaponHandler.transform.position, 100f);
                if (monster == null)
                {
                    target = Vector3.zero;
                    return false;
                }

                target = monster.transform.position;
                return true;
            }

            else
            {
                target = MouseCursorPosFinder.GetMouseWorldPosition();
                return true;
            }
        }
    }
}