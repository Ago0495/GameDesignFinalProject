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
    private bool CanAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(LastInput.x, LastInput.y) * MoveSpeed;
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
