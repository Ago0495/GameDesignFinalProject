using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKBossScript : EnemyScript
{
    public override void Update()
    {
        base.Update();
        if(GetCurrentWeapon() != null)
        {
            WeaponScript weaponScript = GetCurrentWeapon().GetComponent<WeaponScript>();

            if(weaponScript != null)
            {
                if (weaponScript.onCooldown)
                {
                    SwitchWeapon(currentWeaponIndex++);
                }
            }
        }
    }
}
