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

    public IEnumerator projectileLifetime(float time)
    {
        //animation
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (!other.CompareTag("Room") && other.gameObject.layer != gameObject.layer && !other.CompareTag("Hole"))
        {
            Destroy(gameObject);
        }        
    }
}
