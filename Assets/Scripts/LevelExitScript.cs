using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitScript : MonoBehaviour
{
    [SerializeField] GameObject[] doors;
    [SerializeField] bool openDoors;
    [SerializeField] Sprite doorOpenSprite;
    public int goToLevel;
    private LevelManager levelManager;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        if (levelManagerObj != null )
        {
            levelManager = levelManagerObj.GetComponent<LevelManager>();
        }

        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManagerObj != null)
        {
            gameManager = gameManagerObj.GetComponent<GameManager>();
        }

        int currentLevelNumber = levelManager.GetLevelNum();

        Level shopLevel = GameManager.FindInLevels("ShopLevel");
        int shopLevelNumber = 0;
        if (shopLevel != null)
        {
            shopLevelNumber = shopLevel.GetLevelNumber();
        }

        if (goToLevel > 0)
        {
            if (currentLevelNumber == shopLevelNumber)
            {
                SetNextLevel(GameManager.GetNextLevelNumber());
            }
            else
            {
                SetNextLevel(shopLevelNumber);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNextLevel(int level)
    {
        goToLevel = level;
    }

    public void OpenExit()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.GetComponent<SpriteRenderer>().enabled = false;
                door.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void CloseExit()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.GetComponent<SpriteRenderer>().enabled = true;
                door.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.SwitchLevelTo(goToLevel);
        }
    }
}
