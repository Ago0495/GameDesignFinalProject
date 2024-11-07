using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //variables
    public int numEnemies;
    public GameObject[] EnemyPrefabs;
    public Vector2[] EnemyPositions;
    [SerializeField] private GameObject Blocker;

    // Start is called before the first frame update
    void Start()
    {
        Blocker.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerEnter()
    {
        Blocker.SetActive(true);
    }

    public void RoomCleared()
    {
        Blocker.SetActive(false);
    }
}
