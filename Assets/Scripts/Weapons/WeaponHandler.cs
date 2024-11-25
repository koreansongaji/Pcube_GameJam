using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;

public class WeaponHandler : MonoBehaviour
{
    private const int MAX_WEAPON_COUNT = 2;

    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Player player;
    
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.Setup(this);
        }
    }

    private void Update()
    {
        if (GameManager.Pause) return;
        foreach (Weapon weapon in weapons)
        {
            if (weapon.CanAttack() == false)
                continue;
            
            weapon.Attack();
        }
    }
    
    public void AddWeapon(Weapon weapon)
    {
        if (weapons.Count < MAX_WEAPON_COUNT)
        {
            weapons.Add(weapon);
            weapon.Setup(this);
        }
    }
    
    public void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);
    }
}