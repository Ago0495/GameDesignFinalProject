using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class EntityScript : MonoBehaviour
{
    //variables
    [SerializeField] private protected Rigidbody2D rb2d;
    [SerializeField] private protected int hp;
    [SerializeField] private bool indestructible;
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

        stachedWeapons = new List<WeaponScript>();
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
        if (!indestructible)
        {
            hp -= damage;


            if (hp <= 0)
            {
                OnDefeated();
            }
        }

        simpleFlash.Flash();
    }


    private protected virtual void OnDefeated()
    {
        animator.SetBool("IsDead", true);
        moveSpeed = 0;
        isAlive = false;

        weaponHolder.gameObject.SetActive(false);
        weaponStache.gameObject.SetActive(false);
    }

    public void UseWeapon()
    {
        if (currentWeapon != null)
        {
            weaponScript = currentWeapon.GetComponent<WeaponScript>();
            currentWeapon.gameObject.layer = gameObject.layer;
            if (weaponScript != null)
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
        //weaponHolder.rotation = Quaternion.Lerp(weaponHolder.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.deltaTime * rotationSpeed);

        float rotationSpeed;

        if (weaponScript != null)
        {
            rotationSpeed = weaponScript.GetTotalStatPower("handling");
        }
        else
        {
            rotationSpeed = 20;
        }

        if (targetPos.x > 0.5f)
        {
            Quaternion entityRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, entityRotation, Time.deltaTime * rotationSpeed);

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            weaponHolder.rotation = Quaternion.Lerp(weaponHolder.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            weaponHolder.localScale = new Vector3(1, 1, 1);
        }
        else if (targetPos.x < 0.5f)
        {
            Quaternion entityRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, entityRotation, Time.deltaTime * rotationSpeed);

            Quaternion targetRotation = Quaternion.Euler(180, 0, -angle);
            weaponHolder.rotation = Quaternion.Lerp(weaponHolder.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            weaponHolder.localScale = new Vector3(1, 1, 1);
        }

    }

    public virtual void SwitchWeapon(int index)
    {
        if (stachedWeapons.Count != 0)
        {
            index = (currentWeaponIndex % stachedWeapons.Count + stachedWeapons.Count) % stachedWeapons.Count;
        }
        else
        {
            index = 0;
        }


        if (GetNumWeapons() != 0)
        {
            if (currentWeapon != null)
            {
                currentWeapon.SetParent(weaponStache, false);
            }
            currentWeapon = stachedWeapons[index].transform;
            weaponScript = currentWeapon.GetComponent<WeaponScript>();
            currentWeapon.SetParent(weaponHolder, false);
        }
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

        Vector3 moveVector = Vector3.zero;
        float runAnimationSpeed = 0;

        if (GetComponent<NavMeshAgent>())
        {
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            moveVector += navMeshAgent.velocity;
        }

        moveVector += new Vector3(rb2d.velocity.x, rb2d.velocity.y, 0);

        runAnimationSpeed = Mathf.Pow(moveVector.magnitude, 0.5f) * 0.8f;

        if ((rb2d.velocity.x > 0 && spriteRenderer.flipX) || (rb2d.velocity.x < 0 && !spriteRenderer.flipX))
        {
            runAnimationSpeed *= -1f;
        }

        float minSpeedToStartRunAnimation = 0.1f;

        animator.SetBool("IsRunning", Mathf.Abs(moveVector.magnitude) > minSpeedToStartRunAnimation);
        animator.SetFloat("RunSpeed", runAnimationSpeed);
    }

}
