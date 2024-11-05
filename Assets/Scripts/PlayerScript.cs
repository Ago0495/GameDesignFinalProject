using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : EntityScript
{
    //variables
    public GameObject currentWeapon;
    public int Currency;
    public Vector2 LastInput;
    private bool CanAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCollback(InputAction.CallbackContext mv)
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
        if (fb.started && CanAttack)
        {
            CanAttack = false;
            //StartCoroutine(Reload());
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
