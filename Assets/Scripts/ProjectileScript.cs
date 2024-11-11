using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : WeaponScript
{
    private protected override void Start()
    {
        base.Start();

        weaponCollider.enabled = true;
    }

    protected override void ApplyKnockback(Collider2D other, Transform weaponTransform, float atkKnockback)
    {
        other.attachedRigidbody.AddForce(weaponTransform.right * atkKnockback, ForceMode2D.Impulse);
    }
}
