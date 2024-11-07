using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityScript
{
    //variables
    [SerializeField] private int DetectRange;
    [SerializeField] private GameObject target;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        weaponHolder = transform.Find("WeaponHolder");
        currentWeapon = weaponHolder.GetChild(0);

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            AimWeapon(weaponPos, targetPos);
        }
    }

    public void MoveToPlayer()
    {

    }
}
