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
            GameObject area = areaPool.Get(transform);
            area.TryGetComponent(out SingleArea areaScript);
            
            area.transform.position = target;
            areaScript.SetArgs(
                CalculateFinalDamage(),
                CalculateAreaSize(),
                CalculateDuration(), 
                areaPool
            );

            atkTrigger = false;
            StartCoroutine(Cooldown(CalculateCooldown()));
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.AREA;
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
            const float minCooldown = 0.1f;
            
            float ret = weaponData.coolTime * ( 50 / (50 * _player.GetStat().coolTimeReduce.Value));
            
            return Mathf.Max(ret, minCooldown);
        }

        private static bool TryGetTarget(out Vector3 target)
        {
            // 본인 Transform 이내에서 10f 반경 이내 랜덤 점
            target = MouseCursorPosFinder.GetMouseWorldPosition();
            target.y += 1;
            return true;
        }
    }
}