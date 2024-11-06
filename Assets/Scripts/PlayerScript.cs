using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : EntityScript
{
    //variables
    [SerializeField] private GameObject currentWeapon;
    [SerializeField] private int Currency;
    private Vector2 LastInput;
    //private bool CanAttack;
    private Vector3 mousePos;
    private Vector3 weaponPos;
    private Transform weaponHolder;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        weaponHolder = transform.Find("WeaponHolder");
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(LastInput.x, LastInput.y) * moveSpeed;

        //weapon aim
        mousePos = Input.mousePosition;
        weaponPos = Camera.main.WorldToScreenPoint(weaponHolder.position);

        mousePos.z = 10;
        mousePos.x = mousePos.x - weaponPos.x;
        mousePos.y = mousePos.y - weaponPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        weaponHolder.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void MoveCallback(InputAction.CallbackContext mv)
    {
        if (mv.started || mv.performed)
        {
            //movement started or changed
            LastInput = mv.ReadValue<Vector2>();
        }
        else if (mv.canceled)
        {
            //no movement
            LastInput = Vector2.zero;
        }
    }

    public void AttackCallback(InputAction.CallbackContext fb)
    {
        //temp
        canAttack = true;

        if (fb.started && canAttack)
        {
            canAttack = false;
            //StartCoroutine(Reload());

            WeaponScript weaponScript = weaponHolder.GetChild(0).GetComponent<WeaponScript>();
            weaponScript.Attack();
        }

    }

    public void MoveChanged()
    {

    }

    public void SwitchWeapon()
    {

    }

    public void Fire()
    {

    }
}
