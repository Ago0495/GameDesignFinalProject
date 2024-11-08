using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    //variables
    [SerializeField] private int atkDamage;
    [SerializeField] private float atkRange;
    [SerializeField] private float atkCooldown;
    [SerializeField] private float atkKnockbackForce;
    //[SerializeField] private int weaponTag;
    //[SerializeField] private int upgrade;
    private Collider2D weaponCollider;
    private bool onCooldown;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider2D>();

        weaponCollider.enabled = false;
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        if (!onCooldown)
        {
            weaponCollider.enabled = true;

            //start coroutine to turn off collider
            StartCoroutine(WeaponCooldown(atkCooldown));
            onCooldown = true;
        }
    }

    public int getAtkDamage()
    {
        return atkDamage;
    }

    public float GetAtkRange()
    { 
        return atkRange; 
    }

    public float GetAtkCooldown() 
    {  
        return atkCooldown; 
    }

    private IEnumerator WeaponCooldown(float waitTime)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(waitTime);
        weaponCollider.enabled = false;
        Debug.Log("Coroutine ended");
        onCooldown = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        EntityScript otherEntity = other.gameObject.GetComponent<EntityScript>();

        if (otherEntity != null)
        {
            Transform thisWeaponParent = transform.parent;

            if (1<<other.gameObject.layer != 1<< thisWeaponParent.gameObject.layer)
            {
                otherEntity.TakeDamage(atkDamage);
            }
        }
    }
}
