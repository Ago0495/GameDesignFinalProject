using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //variables
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> blockers = new List<GameObject>();
    private int numEnemies;
    private bool visited;


    // Start is called before the first frame update
    void Start()
    {
        visited = false;
        if (enemyPrefabs != null )
        {
            numEnemies = enemyPrefabs.Count;
            foreach (GameObject enemy in enemyPrefabs)
            {
                EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
                RoomScript roomScript = GetComponent<RoomScript>();
                enemyScript.SetRoom(roomScript);
            }
            DeactivateAll(enemyPrefabs);
        }
        else
        {
            Debug.LogWarning("enemyPrefabs is not assigned.");
        }
        DeactivateAll(blockers);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerEnter();
        }
    }

    public void PlayerEnter()
    {
        if (!visited)
        {
            ActivateAll(blockers);
            ActivateAll(enemyPrefabs);
        }

        visited = true;
    }

    public void RoomCleared()
    {
        DeactivateAll(blockers);
    }

    public void DeactivateAll(List<GameObject> roomObjects)
    {
        if (roomObjects.Count > 0)
        {
            foreach (var roomObj in roomObjects)
            {
                roomObj.SetActive(false);
            }
        }
    }
    public void ActivateAll(List<GameObject> roomObjects)
    {
        if (roomObjects.Count > 0)
        {
            foreach (var roomObj in roomObjects)
            {
                roomObj.SetActive(true);
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyPrefabs.Remove(enemy);

        numEnemies = enemyPrefabs.Count;
        if (numEnemies <= 0)
        {
            DeactivateAll(blockers);
        }
    }
}
