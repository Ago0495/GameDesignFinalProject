using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //variables
    public int numEnemies;
    public GameObject[] EnemyPrefabs;
    public Vector2[] EnemyPositions;
    [SerializeField] private GameObject[] Blockers;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var blocker in Blockers)
        {
            blocker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerEnter()
    {
        foreach (var blocker in Blockers)
        {
            blocker.SetActive(true);
        }
    }

    public void RoomCleared()
    {
        foreach (var blocker in Blockers)
        {
            blocker.SetActive(false);
        }
    }
}
