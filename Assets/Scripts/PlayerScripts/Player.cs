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
        // todo : 플레이어 사망 처리
    }

    public PlayerData GetStat()
    {
        return runtimeData;
    }
}
