namespace Weapons
{
    public class MeleeWeapon : Weapon
    { 
        private Player _player;
        public override void Setup(WeaponHandler handler)
        {
        }

        public override void Attack()
        {
        }

        protected override float CalculateFinalDamage()
        {
            return 0;
        }
    }
}