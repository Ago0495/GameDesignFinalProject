 using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //variables
    [SerializeField] private int currentLevel;
    [SerializeField] private Level[] levels;
    private Scene currentScene;
    private PlayerScript playerScript;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        LevelManager levelManagerScript = levelManagerObj.GetComponent<LevelManager>();

        currentLevel = levelManagerScript.GetLevelNum();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchLevelTo(int levelNum)
    {
        switch(levelNum) 
        {
            case -1:
                SceneManager.LoadScene("TestLevel");
                break;
            case 0:
                SceneManager.LoadScene("MainTitleScene");
                break;
            case 1:
                SceneManager.LoadScene("ShopLevel");
                break;
            default:
                SceneManager.LoadScene("MainTitleScene");
                break;
        }
    }
}
