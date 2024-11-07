using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    //variables
    [SerializeField] private protected Rigidbody2D rb2d;
    [SerializeField] private protected int hp;
    [SerializeField] private protected bool canAttack;
    [SerializeField] private protected float moveSpeed;
    private protected Vector3 weaponPos;
    private protected Transform weaponHolder;
    private protected Transform currentWeapon;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {

        }
    }

    public void TakeDamage(GameObject obj)
    {

    }


    public void OnDefeated()
    {
        Destroy(this.gameObject);
    }

    public void UseWeapon()
    {
        if (canAttack)
        {
            canAttack = false;
            //StartCoroutine(Reload());

            WeaponScript weaponScript = currentWeapon.GetComponent<WeaponScript>();
            if (weaponScript != null )
            {
                weaponScript.Attack();
            }
        }
    }

    public void AimWeapon(Vector3 weaponPos, Vector3 targetPos)
    {
        //weapon aim
        targetPos.x = targetPos.x - weaponPos.x;
        targetPos.y = targetPos.y - weaponPos.y;

        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        weaponHolder.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
