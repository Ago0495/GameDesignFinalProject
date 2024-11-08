using UnityEngine;
using UnityEngine.AI;

public class CoinScript : MonoBehaviour
{
    //variables
    [SerializeField] private int DetectRange;
    [SerializeField] private GameObject target;
    [SerializeField] private protected float moveSpeed;
    private Vector3 targetPos;
    private Vector3 targetDir;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player");
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    public void Update()
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            MoveToTarget();
        }
    }

    public void MoveToTarget()
    {
        agent.SetDestination(new Vector3(targetPos.x, targetPos.y, transform.position.z));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.GetComponent<PlayerScript>().ChangeCurrency(1);
            Destroy(this.gameObject);
        }
    }
}
