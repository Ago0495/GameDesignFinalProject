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
    private NavMeshAgent agent;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        agent.speed = moveSpeed;

        if (target != null)
        {
            targetPos = target.transform.position;
            weaponPos = weaponHolder.transform.position;

            AimWeapon(weaponPos, targetPos);
            MoveToTarget();
        }
    }

    public void MoveToTarget()
    {
        agent.SetDestination(new Vector3(targetPos.x, targetPos.y, transform.position.z));
    }
}
