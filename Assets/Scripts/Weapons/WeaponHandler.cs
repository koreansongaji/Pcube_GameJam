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
        weapons = new List<Weapon>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        foreach (Weapon weapon in weapons.Where(weapon => weapon.CanAttack()))
        {
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