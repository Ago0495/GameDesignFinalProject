using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //variables
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> blockers;
    public int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = enemyPrefabs.Count;
        DeactivateAll(blockers);
        DeactivateAll(enemyPrefabs);
    }

    // Update is called once per frame
    void Update()
    {
        numEnemies = enemyPrefabs.Count;
        if (numEnemies <= 0)
        {
            DeactivateAll(blockers);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");
        if (other.gameObject.tag == "Player")
        {
            PlayerEnter();
        }
    }

    public void PlayerEnter()
    {
        ActivateAll(blockers);
        ActivateAll(enemyPrefabs);
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
}
