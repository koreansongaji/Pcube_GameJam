using Data;
using Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.WeaponSubScripts;

namespace Weapons
{
    public class Leaves : Weapon
    {
        private Player _player;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private AimType currentType;

        private GameObjectPool _bulletPool;
        
        public override void Setup(WeaponHandler handler)
        {
            weaponHandler = handler;
            _player = handler.GetComponent<Player>();
            _bulletPool = new GameObjectPool(bulletPrefab, handler.transform, 10);
            atkTrigger = true;
        }

        public override void Attack()
        {
            if(TryGetTarget(out Vector3 target) == false)
                return;
            
            // shoot LeavesBullet
            GameObject bullet = _bulletPool.Get();
            bullet.TryGetComponent(out NormalBullet bulletScript);
            
            bullet.transform.position = weaponHandler.transform.position + new Vector3(0, 1, 0);
            bulletScript.SetTarget(target);
            bulletScript.SetArgs(CalculateFinalDamage(), CalculateProjectileSpeed(), _bulletPool);
            
            
            atkTrigger = false;
            StartCoroutine(Cooldown(CalculateCooldown()));
        }

        protected override float CalculateFinalDamage()
        {
            float percentage = _player.GetStat().damage.Value;
            
            float damage = weaponData.damage * ((100 + percentage) * 0.01f);
            return damage;
        }
        private float CalculateProjectileSpeed()
        {
            return weaponData.projectileSpeed * 
                   (100 + _player.GetStat().projectileSpeed.Value) * 0.01f;
        }
        private float CalculateCooldown()
        {
            return weaponData.coolTime * 
                   (100 - _player.GetStat().coolTimeReduce.Value) * 0.01f
                   / (_player.GetStat().projectileCount.Value + weaponData.projectileCount);
        }
        
        private void ReleaseBullet(GameObject bullet)
        {
            _bulletPool.Release(bullet);
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

            target = MouseCursorPosFinder.GetMouseWorldPosition();
            return true;
        }
    }
    
    public enum AimType
    {
        TARGET,
        DIRECTION
    }
}