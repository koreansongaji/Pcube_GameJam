using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData baseData;
    [SerializeField] private PlayerData runtimeData;

    private PlayerLevel _playerLevel;
    private WeaponHandler _weaponHandler;

    public int[] skill1 = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] skill2 = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] skill3 = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    
    public int statePoint;
    private void Awake()
    {
        _playerLevel = GetComponent<PlayerLevel>();
        _weaponHandler = GetComponent<WeaponHandler>();

        runtimeData = Instantiate(baseData);
        runtimeData.currentHp.baseValue = baseData.maxHp.baseValue;
    }

    public void TakeDamage(float damage)
    {
        runtimeData.currentHp.baseValue -= damage;
        if (Health() <= 0)
        {
            Die();
        }

        return;

        float Health() => runtimeData.currentHp.baseValue;
    }

    private void Die()
    {
        // todo : �÷��̾� ��� ó��
    }

    public PlayerData GetStat()
    {
        return runtimeData;
    }
}
