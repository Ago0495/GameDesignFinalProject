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
    [SerializeField] int goToLevel;
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
    }

    // Update is called once per frame
    void Update()
    {
        //test, remove on build
        if (openDoors)
        {
            OpenExit();
        }
        else
        {
            CloseExit();
        }
    }

    public void OpenExit()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                Sprite doorSprite = door.GetComponent<SpriteRenderer>().sprite;
                doorSprite = doorOpenSprite;
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
                Sprite doorSprite = door.GetComponent<Sprite>();
                //doorSprite = doorOpenSprite;
                door.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.SwitchLevelTo(goToLevel);
        }
    }
}
