using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType
{
    gun,
    sword,
    wand
}
public class Weapon : MonoBehaviour
{
    public weaponType wpType;


    private void Start()
    {
        WeaponDecidingFactor();
    }
    private void Update()
    {
        WeaponDecidingFactor();
        SwapWeaponType();

    }

    private void SwapWeaponType()
    {
        if (Input.GetKeyDown(KeyCode.X))//mele
        {
            wpType = weaponType.sword;
        }
        if (Input.GetKeyDown(KeyCode.Z))//ranged gun
        {
            wpType = weaponType.gun;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            wpType = weaponType.wand;
        }
    }

    private void WeaponDecidingFactor()
    {
        switch (wpType)
        {
            case weaponType.gun:
                GetComponent<SwordHand>().enabled = false;
                GetComponent<GunHand>().enabled   = true;
                GetComponent<WandHand>().enabled  = false;
                break;
            case weaponType.sword:
                GetComponent<SwordHand>().enabled = true;
                GetComponent<GunHand>().enabled   = false;
                GetComponent<WandHand>().enabled  = false;
                break;
            case weaponType.wand:
                GetComponent<SwordHand>().enabled = false;
                GetComponent<GunHand>().enabled   = false;
                GetComponent<WandHand>().enabled  = true;
                break;
            default:
                break;
        }
    }
}
