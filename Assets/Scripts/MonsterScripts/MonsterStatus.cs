using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MonsterData�� �޾Ƽ� ���� �������� ��ȯ
/// </summary>
public class MonsterStatus : MonoBehaviour
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
            if (_maxHp < 0) _maxHp = 0;
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
            if (Damage < 0) Damage = 0;
        }
    }
    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value;
            if (AttackSpeed < 0) AttackSpeed = 0;
        }
    }

    [SerializeField] private MonsterData monsterData;
    /// <summary>
    /// ���� �޾ƿ��� �Լ�, Awake������ �ѹ� ����
    /// </summary>
    private void GetInfo()
    {
        Name = monsterData.Name;
        Speed = monsterData.Speed;
        MaxHp = monsterData.MaxHp;
        CurHp = MaxHp;
        Damage = monsterData.Damage;
        AttackSpeed = monsterData.AttackSpeed;
    }
    private void Awake()
    {
        GetInfo();
    }

}
