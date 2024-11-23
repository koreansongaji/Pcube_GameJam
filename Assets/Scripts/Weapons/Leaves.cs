using Data;
using Generic;
using Helpers;
using UnityEngine;
using Weapons.WeaponSubScripts;

namespace Weapons
{
    public class Leaves : Weapon
    {
        private Player _player;
        [SerializeField] private GameObject bulletPrefab;
        
        private GameObjectPool _bulletPool;
        private AimType _currentType;
        
        
        
        public override void Setup(WeaponHandler handler)
        {
            weaponHandler = handler;
            _player = handler.GetComponent<Player>();
            _bulletPool = new GameObjectPool(bulletPrefab, handler.transform, 10);
            atkTrigger = true;
        }
        public override void Attack()
        {
            MonsterBehavior monster = 
                NearestMonsterFinder.
                    FindNearestMonster(weaponHandler.transform.position, 100f);
            if (monster == null)
            {
                Debug.Log("No monster found");
                return;
            }
            
            // shoot LeavesBullet
            GameObject bullet = _bulletPool.Get();
            bullet.TryGetComponent(out NormalBullet bulletScript);
            
            bullet.transform.position = weaponHandler.transform.position;
            bulletScript.SetTarget(monster.transform.position);
            bulletScript.SetArgs(CalculateFinalDamage(), CalculateProjectileSpeed(), _bulletPool);
            
            
            atkTrigger = false;
            StartCoroutine(Cooldown(CalculateCooldown()));
        }
        public override bool CanUpgrade()
        {
            throw new System.NotImplementedException();
        }
        public override void Upgrade()
        {
            throw new System.NotImplementedException();
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

        private Vector3 GetTarget()
        {
            if (_currentType == AimType.TARGET)
            {
                var monster = NearestMonsterFinder.FindNearestMonster(weaponHandler.transform.position, 100f);
                if (monster == null)
                {
                    Debug.Log("No monster found");
                    return Vector3.zero;
                }
            }

            else
            {
                return MouseCursorPosFinder.GetMouseWorldPosition();
            }
            
            return Vector3.zero;
        }
    }
    
    public enum AimType
    {
        TARGET,
        DIRECTION
    }
}