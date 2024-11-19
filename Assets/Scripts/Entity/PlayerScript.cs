using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : EntityScript
{
    //variables
    [SerializeField] private int currency;
    [SerializeField] private int maxWeapons;
    private Vector2 lastInput;
    private Vector3 mousePos;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (isAlive)
        {
            //rb2d.velocity = new Vector2(lastInput.x, lastInput.y) * moveSpeed;

            rb2d.AddForce(lastInput * moveSpeed * Time.deltaTime);

            mousePos = Input.mousePosition;
            weaponPos = Camera.main.WorldToScreenPoint(weaponHolder.position);
            mousePos.z = 10;

            AimWeapon(weaponPos, mousePos);
        }
    }


    public void OnMove(InputAction.CallbackContext mv)
    {
        if (mv.started || mv.performed)
        {
            //movement started or changed
            lastInput = mv.ReadValue<Vector2>();
        }
        else if (mv.canceled)
        {
            //no movement
            lastInput = Vector2.zero;
        }
    }

    public void OnFire(InputAction.CallbackContext fb)
    {
        canAttack = true;

        if (fb.started && isAlive)
        {
            UseWeapon();
        }
    }

    public void OnScroll(InputAction.CallbackContext sc)
    {
        if (sc.performed)
        {
            float rawVal = sc.ReadValue<float>();
            int val = (int)(rawVal / Mathf.Abs(rawVal));
            currentWeaponIndex += val;

            SwitchWeapon(currentWeaponIndex);
        }
    }

    public override void SwitchWeapon(int index)
    {
        base.SwitchWeapon(index);
    }

    public int GetCurrency()
    {
        return currency;
    }

    public int GetMaxWeapons()
    {
        return maxWeapons;
    }
    public void ChangeCurrency(int amount)
    {
        currency += amount;
    }
}
