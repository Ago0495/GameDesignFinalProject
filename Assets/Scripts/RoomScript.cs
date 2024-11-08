using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //variables
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> blockers;
    private int numEnemies;
    private bool visited;


    // Start is called before the first frame update
    void Start()
    {
        visited = false;
        numEnemies = enemyPrefabs.Count;
        DeactivateAll(blockers);
        DeactivateAll(enemyPrefabs);

        foreach (GameObject enemy in enemyPrefabs)
        {
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            RoomScript roomScript = GetComponent<RoomScript>();
            enemyScript.SetRoom(roomScript);
        }
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
        foreach (var roomObj in roomObjects)
        {
            roomObj.SetActive(false);
        }
    }
    public void ActivateAll(List<GameObject> roomObjects)
    {
        foreach (var roomObj in roomObjects)
        {
            roomObj.SetActive(true);
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
