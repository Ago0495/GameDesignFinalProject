using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityScript
{
    //variables
    [SerializeField] private int DetectRange;
    [SerializeField] private GameObject target;
    [SerializeField] private List<GameObject> createOnDefeat;
    private Vector3 targetPos;
    private Vector3 targetDir;
    private NavMeshAgent agent;
    private RoomScript room;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindGameObjectWithTag("Player");
        weaponScript = currentWeapon.GetComponent<WeaponScript>();
        if (weaponScript == null)
        {
            agent.stoppingDistance = 0;
        }
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

            TryAttack();
        }
    }

    public void MoveToTarget()
    {
        agent.SetDestination(new Vector3(targetPos.x, targetPos.y, transform.position.z));
        agent.stoppingDistance = weaponScript.GetAtkRange() * 0.5f;
    }

    public void TryAttack()
    {
        //checks if player is inrange of weapon
        weaponScript = currentWeapon.GetComponent<WeaponScript>();
        targetDir = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, weaponScript.GetAtkRange(), 1 << target.layer);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Player")
            {
                UseWeapon();
            }
        }
    }

    private protected override void OnDefeated()
    {
        foreach (GameObject obj in createOnDefeat)
            {
                Instantiate(obj, transform.position, Quaternion.identity);
            }

        //remove itself from room list
        if (room != null)
        {
            room.RemoveEnemy(gameObject);
        }
        Destroy(gameObject);
    }
    public void SetRoom(RoomScript _room)
    {
        room = _room;
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.DrawLine(transform.position, transform.position + targetDir * weaponScript.GetAtkRange());
        }
    }
}
