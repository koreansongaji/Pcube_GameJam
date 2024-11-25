using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData baseData;
    [SerializeField] private PlayerData runtimeData;
    [SerializeField] private float curHp;

    private PlayerLevel _playerLevel;
    private WeaponHandler _weaponHandler;
    private void Awake()
    {
        _playerLevel = GetComponent<PlayerLevel>();
        _weaponHandler = GetComponent<WeaponHandler>();

        runtimeData = Instantiate(baseData);
        curHp = baseData.maxHp.baseValue;
    }

    private void Update()
    {
        // regen
        if (curHp < runtimeData.maxHp.Value)
        {
            Heal(runtimeData.hpRegen.Value * Time.deltaTime);
        }
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
    
    public void Heal(float amount)
    {
        Debug.Log($"Heal : {amount}");
        Debug.Log("Current HP : " + curHp);
        
        curHp += amount;
        curHp = Mathf.Clamp(curHp, 0, runtimeData.maxHp.Value);
        
        Debug.Log("After HP : " + curHp);
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
