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

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider2D>();

        weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        Debug.Log("Weapon Attack!");
        weaponCollider.enabled = true;
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
}
