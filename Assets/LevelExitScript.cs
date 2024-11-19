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
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        LevelManager levelManager = null;
        if (levelManagerObj != null )
        {
            levelManager = levelManagerObj.GetComponent<LevelManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                Sprite doorSprite = door.GetComponent<Sprite>();
                //doorSprite = doorOpenSprite;
                door.gameObject.SetActive(false);
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
                door.gameObject.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(goToLevel);
        }
    }
}
