using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinScript : EntityScript
{
    //variables
    public int DetectRange;
    private NavMeshAgent NavAgent;

    // Start is called before the first frame update
    public override void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public void MoveToPlayer()
    {

    }
}
