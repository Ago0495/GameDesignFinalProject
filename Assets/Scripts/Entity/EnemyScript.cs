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
    private Vector3 targetDir;
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

            weaponScript = currentWeapon.GetComponent<WeaponScript>();
            targetDir = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, weaponScript.GetAtkRange(), 1 << 7);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "Player")
                {
                    UseWeapon();
                }
            }
        }
    }

    public void MoveToTarget()
    {
        agent.SetDestination(new Vector3(targetPos.x, targetPos.y, transform.position.z));
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.DrawLine(transform.position, targetDir * weaponScript.GetAtkRange());
        }
    }
}
