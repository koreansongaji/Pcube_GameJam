using UnityEngine;

namespace Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
    public class WeaponData : ScriptableObject
    {
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                UnityEditor.EditorUtility.SetDirty(this); // 변경사항 무효화
            }
        }
#endif
        
        public float damage;
        public float projectileSpeed;
        public float duration;
        public float attackRange;
        public float coolTime;
        public float projectileCount;
    }
}

public enum WeaponStatType
{
    DAMAGE,
    PROJECTILE_SPEED,
    DURATION,
    ATTACK_RANGE,
    COOL_TIME,
    PROJECTILE_COUNT,
    RESURRECTION,
    GROWTH
}