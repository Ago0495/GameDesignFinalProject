using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKBossScript : EnemyScript
{
    [SerializeField] private float weaponSwitchWaitTime;
    [SerializeField] private bool onSwitchCooldown;

    public override void Update()
    {
        base.Update();
        if(GetCurrentWeapon() != null)
        {
            if(weaponScript != null)
            {
                if (weaponScript.onCooldown && !onSwitchCooldown)
                {
                    StartCoroutine(WaitToSwitchWeapon(weaponSwitchWaitTime));
                    onSwitchCooldown = true;
                }
            }
        }
    }

    IEnumerator WaitToSwitchWeapon(float waitTime)
    {
        SwitchWeapon(currentWeaponIndex++);

        yield return new WaitForSeconds(waitTime);

        onSwitchCooldown = false;
    }
}
