using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RoomScript : MonoBehaviour
{
    //variables
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> blockers;
    //[SerializeField] private GameObject levelManager;
    private int numEnemies;
    private bool visited;
    [SerializeField] private LevelManager levelScript;


    // Start is called before the first frame update
    void Start()
    {
        visited = false;
        numEnemies = enemyPrefabs.Count;
        DeactivateAll(blockers);
        //DeactivateAll(enemyPrefabs);

        foreach (GameObject enemy in enemyPrefabs)
        {
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            RoomScript roomScript = GetComponent<RoomScript>();
            enemyScript.SetRoom(roomScript);

            enemyScript.SetTarget(enemyScript.gameObject);
            enemyScript.SetIndestructible(true);
        }
        GameObject levelObj = GameObject.FindGameObjectWithTag("LevelManager");
        levelScript = levelObj.GetComponentInParent<LevelManager>();

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
            //ActivateAll(enemyPrefabs);
            
            foreach (GameObject enemy in enemyPrefabs)
            {
                EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
                GameObject target = GameObject.FindGameObjectWithTag("Player");
                enemyScript.SetTarget(target);
                enemyScript.SetIndestructible(false);
            }
        }

        visited = true;
    }

    public void RoomCleared()
    {
        DeactivateAll(blockers);
        levelScript.clearedRoom();
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
            RoomCleared();
        }
    }
}
