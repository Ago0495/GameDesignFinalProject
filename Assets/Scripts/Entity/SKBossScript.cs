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
                float targetDist = (targetPos - transform.position).magnitude;
                Debug.Log(targetDist);
                if (targetDist < 2 && GetCurrentWeapon().GetComponent<RangedWeaponScript>() != null && !onSwitchCooldown)
                {
                    StartCoroutine(WaitToSwitchWeapon(weaponSwitchWaitTime));
                    onSwitchCooldown = true;
                }
                else if (GetCurrentWeapon().GetComponent<RangedWeaponScript>() == null && !onSwitchCooldown)
                {
                    StartCoroutine(WaitToSwitchWeapon(weaponSwitchWaitTime));
                    onSwitchCooldown = true;
                }

                if (GetCurrentWeapon().GetComponent<RangedWeaponScript>() != null)
                {
                    moveSpeed = 4;
                }
                else
                {
                    moveSpeed = 2;
                }
            }
        }
    }

    IEnumerator WaitToSwitchWeapon(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SwitchWeapon(currentWeaponIndex++);

        onSwitchCooldown = false;
    }
}
