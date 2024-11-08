using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private protected WeaponScript weaponScript;
    private float angle;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponHolder = transform.Find("WeaponHolder");
        currentWeapon = weaponHolder.GetChild(0);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        layerSort();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            OnDefeated();
        }
    }


    private protected virtual void OnDefeated()
    {
        Debug.Log("Entity defeated");
    }

    public void UseWeapon()
    {
        weaponScript = currentWeapon.GetComponent<WeaponScript>();
        if (weaponScript != null )
        {
            weaponScript.Attack();
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

    public void layerSort()
    {
        spriteRenderer.sortingOrder = -1 * (int)(transform.position.y * 10);
    }
}
