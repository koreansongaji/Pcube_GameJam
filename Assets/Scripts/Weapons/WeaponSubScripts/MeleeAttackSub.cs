using Data;
using Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Weapons.WeaponSubScripts;

public class MeleeAttackSub : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag($"Monster"))
            {
                if(other.TryGetComponent(out MonsterBehavior monster) == false)
                    return;
                transform.parent.GetComponent<MeleeAttack>().Damage(monster);
            }
        }
    }