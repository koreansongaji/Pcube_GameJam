using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // armor 곱연산 계산. log로
        damage *= 100 / (100 + runtimeData.armor.Value);
        
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
        SceneManager.LoadScene("GameOver");
    }

    public PlayerData GetStat()
    {
        return runtimeData;
    }
}
