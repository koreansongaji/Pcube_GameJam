using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
/// <summary>
/// 몬스터들의 기본 데이터(스탯)을 저장하는 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    [SerializeField]
    private float _speed;
    public float Speed
    {
        get { return _speed; }
        set 
        {
            _speed = value;
            if (_speed < 0) _speed = 0;
        }
    }
    [SerializeField]
    private float _maxHp;
    public float MaxHp
    {
        get { return _maxHp; }
        set 
        { 
            _maxHp = value;
            if(_maxHp < 0) _maxHp = 0;
        }
    }
    [SerializeField]
    private float _curHp;
    public float CurHp
    {
        get { return _curHp; }
        set
        {
            _curHp = value;
            if (_curHp < 0) _curHp = 0;
        }
    }

    [SerializeField]
    private float _damage;
    public float Damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            if(Damage < 0) Damage = 0;
        }
    }
    /// <summary>
    /// 공격 속도
    /// </summary>
    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value;
            if(AttackSpeed < 0) AttackSpeed = 0;
        }
    }
    /// <summary>
    /// 공격 사거리
    /// </summary>
    [SerializeField]
    private float _range;
    public float Range
    {
        get { return _range; }
        set
        {
            _range = value;
            if(Range < 0) Range = 0;
        }
    }
    [SerializeField]
    private int _kind;
    public int Kind
    {
        get { return _kind; }
        set { _kind = value; }
    }
}
