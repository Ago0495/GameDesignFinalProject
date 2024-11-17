using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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
    private protected Transform weaponStache;
    private protected Transform currentWeapon;
    [SerializeField] private protected List<WeaponScript> stachedWeapons;
    [SerializeField] private protected int currentWeaponIndex;
    private protected WeaponScript weaponScript;
    private float angle;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SimpleFlash simpleFlash;
    private protected bool isAlive;

    // Start is called before the first frame update
    public virtual void Start()
    {
        isAlive = true;

        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        simpleFlash = GetComponent<SimpleFlash>();
        weaponHolder = transform.Find("WeaponHolder");
        weaponStache = transform.Find("WeaponStache");

        stachedWeapons = weaponHolder.GetComponentsInChildren<WeaponScript>().ToList();
        stachedWeapons = stachedWeapons.Concat(weaponStache.GetComponentsInChildren<WeaponScript>()).ToList();

        if (stachedWeapons.Count > 0 )
        {
            currentWeaponIndex = 0;
            currentWeapon = stachedWeapons[currentWeaponIndex].transform;
            weaponScript = currentWeapon.GetComponent<WeaponScript>();
            SwitchWeapon(currentWeaponIndex);
        }
    }
    public virtual void Update()
    {
        UpdateAnimator();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        simpleFlash.Flash();

        if (hp <= 0)
        {
            OnDefeated();
        }
    }


    private protected virtual void OnDefeated()
    {
        animator.SetBool("IsDead", true);
        moveSpeed = 0;
        isAlive = false;
    }

    public void UseWeapon()
    {
        weaponScript = currentWeapon.GetComponent<WeaponScript>();
        currentWeapon.gameObject.layer = gameObject.layer;
        if (weaponScript != null)
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
        //weaponHolder.rotation = Quaternion.Lerp(weaponHolder.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.deltaTime * weaponScript.GetTotalStatPower("handling"));

        if (targetPos.x > 0.1f)
        {
            spriteRenderer.flipX = false;
            weaponHolder.rotation = Quaternion.Lerp(weaponHolder.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.deltaTime * weaponScript.GetTotalStatPower("handling"));
        }
        else if (targetPos.x < 0.1f)
        {
            spriteRenderer.flipX = true;
            weaponHolder.rotation = Quaternion.Lerp(weaponHolder.rotation, Quaternion.Euler(new Vector3(180, 0, -angle)), Time.deltaTime * weaponScript.GetTotalStatPower("handling"));
        }
    }

    public virtual void SwitchWeapon(int index)
    {
        currentWeapon.SetParent(weaponStache, false);
        currentWeapon = stachedWeapons[index].transform;
        weaponScript = currentWeapon.GetComponent<WeaponScript>();
        currentWeapon.SetParent(weaponHolder, false);
    }

    public void AddWeaponToEntity(GameObject _newWeapon)
    {
        GameObject newWeapon = Instantiate(_newWeapon, weaponStache);
        WeaponScript newWeaponScript = newWeapon.GetComponent<WeaponScript>();
        stachedWeapons.Add(newWeaponScript);
        currentWeaponIndex = GetNumWeapons() - 1;
        SwitchWeapon(currentWeaponIndex);
    }

    public Transform GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public WeaponScript GetStachedWeapon(int index)
    {
        index = (index % stachedWeapons.Count + stachedWeapons.Count) % stachedWeapons.Count;
        return stachedWeapons[index];
    }

    public int GetCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }
    public int GetNumWeapons()
    {
        return stachedWeapons.Count;
    }
    private void UpdateAnimator()
    {
        if (animator == null) return;

        float speed = 0;

        if (GetComponent<NavMeshAgent>())
        {
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            speed += Mathf.Clamp(navMeshAgent.velocity.magnitude, 0f, 1f);
        }
        
        speed += Mathf.Clamp(rb2d.velocity.magnitude, 0f, 1f);

        if ((rb2d.velocity.x > 0 && spriteRenderer.flipX) || (rb2d.velocity.x < 0 && !spriteRenderer.flipX))
        {
            speed *= -1f;
        }

        animator.SetBool("IsRunning", Mathf.Abs(speed) > 0.1f);
        animator.SetFloat("RunSpeed", speed);
    }

}
