using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedWeaponScript : WeaponScript
{
    [SerializeField] GameObject projectilePrefab;

    public override void Attack()
    {
        if (!onCooldown)
        {
            float handling = GetTotalStatPower("handling");
            float range = GetTotalStatPower("atkRange");

            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.parent.rotation);
            ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
            Rigidbody2D projectileRb2D = projectile.GetComponent<Rigidbody2D>();

            projectile.layer = gameObject.layer;

            projectileScript.SetBaseStats(weaponBaseStats);

            foreach (Upgrade upgrade in weaponUpgrades)
            {
                projectileScript.AddUpgrade(upgrade);
            }

            projectileRb2D.velocity = transform.parent.right * handling;

            projectileScript.StartCoroutine(projectileScript.projectileLifetime(range * 0.1f));

            StartCoroutine(WeaponCooldown((10f / handling) * attackAnimationTime));
            onCooldown = true;

            //play swing animation
            animator.SetFloat("atkSpeed", (handling / 10f));
            //animator.SetBool("attack", true);
            animator.Play("RangedAttackAnimation");
        }
    }
}
