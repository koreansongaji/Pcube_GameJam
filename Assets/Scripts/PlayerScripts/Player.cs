using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData baseData;
    [SerializeField] private PlayerData runtimeData;
    [SerializeField ]private float curHp;

    private PlayerLevel _playerLevel;
    private WeaponHandler _weaponHandler;
    private void Awake()
    {
        _playerLevel = GetComponent<PlayerLevel>();
        _weaponHandler = GetComponent<WeaponHandler>();

        runtimeData = Instantiate(baseData);
        curHp = baseData.maxHp.baseValue;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("TakeDamage 함수 실행");
        curHp -= damage;
        if (curHp <= 0)
        {
            Die();
        }
    }

    public float GetHealth()
    {
        return curHp;
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
