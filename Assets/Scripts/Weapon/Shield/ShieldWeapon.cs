using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : Weapon
{
    protected override void Start()
    {
        base.Start();

        InitSettings();
    }
    
    private void InitSettings()
    {
        ChangeCurWeaponAttackType(0);
    }

    protected override void Attack(int attackCount)
    {
        
    }
}
