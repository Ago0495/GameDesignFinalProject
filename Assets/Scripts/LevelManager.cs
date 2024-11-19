using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //variables
    [SerializeField] private RoomScript[] rooms;
    [SerializeField] private LevelExitScript levelExitScript;
    [SerializeField] private bool levelComplete;
    [SerializeField] private int currentLevel;
    [SerializeField] private int roomsCleared;
    // Start is called before the first frame update
    void Start()
    {
        if (levelExitScript == null)
        {
            GameObject levelExitObj = GameObject.FindGameObjectWithTag("LevelExit");
            if (levelExitObj != null )
            {
                levelExitScript = levelExitObj.GetComponent<LevelExitScript>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (levelComplete)
        {
            SetLevelComplete(true);
        }
    }

    public void SetLevelComplete(bool levelComplete)
    {
        this.levelComplete = levelComplete;
        OpenLevelExit();
    }
    
    public void OpenLevelExit()
    {
        levelExitScript.OpenExit();
    }
}
