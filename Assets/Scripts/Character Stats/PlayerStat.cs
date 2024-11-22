using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    // 최대 체력, 초당 재생, 방어력, 피해량, 투사체 속도, 지속시간, 공격범위, 쿨타임, 투사체 수, 부활, 성장
    [SerializeField]
    private float _maxHp;
    public float MaxHp
    {
        get => _maxHp;
        set
        {
            _maxHp = value;
            if (_maxHp < 0) _maxHp = 0;
        }
    }
    
    [SerializeField]
    private float _hpRegen;
    public float HpRegen
    {
        get => _hpRegen;
        set
        {
            _hpRegen = value;
            if (_hpRegen < 0) _hpRegen = 0;
        }
    }
    
    [SerializeField]
    private float _defense;
    public float Defense
    {
        get => _defense;
        set
        {
            _defense = value;
            if (_defense < 0) _defense = 0;
        }
    }
    
    [SerializeField]
    private float _damage;
    public float Damage
    {
        get => _damage;
        set
        {
            _damage = value;
            if (_damage < 0) _damage = 0;
        }
    }
    
    [SerializeField]
    private float _projectileSpeed;
    public float ProjectileSpeed
    {
        get => _projectileSpeed;
        set
        {
            _projectileSpeed = value;
            if (_projectileSpeed < 0) _projectileSpeed = 0;
        }
    }
    
    [SerializeField]
    private float _duration;
    public float Duration
    {
        get => _duration;
        set
        {
            _duration = value;
            if (_duration < 0) _duration = 0;
        }
    }
    
    [SerializeField]
    private float _attackRange;
    public float AttackRange
    {
        get => _attackRange;
        set
        {
            _attackRange = value;
            if (_attackRange < 0) _attackRange = 0;
        }
    }
    
    [SerializeField]
    private float _coolTime;
    public float CoolTime
    {
        get => _coolTime;
        set
        {
            _coolTime = value;
            if (_coolTime < 0) _coolTime = 0;
        }
    }
    
    [SerializeField]
    private float _projectileCount;
    public float ProjectileCount
    {
        get => _projectileCount;
        set
        {
            _projectileCount = value;
            if (_projectileCount < 0) _projectileCount = 0;
        }
    }
    
    [SerializeField]
    private float _resurrection;
    public float Resurrection
    {
        get => _resurrection;
        set
        {
            _resurrection = value;
            if (_resurrection < 0) _resurrection = 0;
        }
    }
    
    [SerializeField]
    private float _growth;
    public float Growth
    {
        get => _growth;
        set
        {
            _growth = value;
            if (_growth < 0) _growth = 0;
        }
    }
}