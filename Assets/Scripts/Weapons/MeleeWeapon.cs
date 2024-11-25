using Generic;
using Helpers;
using UnityEngine;
using Weapons.WeaponSubScripts;

namespace Weapons
{
    public class MeleeWeapon : Weapon
    { 
        private Player _player;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private AimType currentType;
        
        public override void Setup(WeaponHandler handler)
        {
            weaponHandler = handler;
            _player = handler.GetComponent<Player>();
            currentType = AimType.DIRECTION;
        }

        public override void Attack()
        {
            if (TryGetTarget(out Vector3 target) == false)
                return;

            // shoot LeavesBullet
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.TryGetComponent(out MeleeAttack bulletScript);

            bullet.transform.position = weaponHandler.transform.position;
            bulletScript.SetTarget(target);
            bulletScript.SetArgs(
                CalculateFinalDamage(),
                CalculateWeaponRange()
            );

            atkTrigger = false;
            StartCoroutine(Cooldown(CalculateCooldown()));
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.MELEE;
        }

        protected override float CalculateFinalDamage()
        {
            float percentage = _player.GetStat().damage.Value;

            float damage = weaponData.damage * ((100 + percentage) * 0.01f);
            return damage;
        }

        private float CalculateWeaponRange()
        {
            return weaponData.attackRange * 
                   ((100 + _player.GetStat().attackRange.Value) * 0.01f);
        }

        private float CalculateCooldown()
        {
            return weaponData.coolTime *
                   (100 - _player.GetStat().coolTimeReduce.Value) * 0.01f
                   / (_player.GetStat().projectileCount.Value + weaponData.projectileCount);
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

        public enum AimType
        {
            TARGET,
            DIRECTION
        }
    }
}

