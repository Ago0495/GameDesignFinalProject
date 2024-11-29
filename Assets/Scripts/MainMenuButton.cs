using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void OnClick()
    {
        GameObject player = GameObject.Find("/Player");
        Destroy(player);

        GameObject gamemanager = GameObject.Find("/GameManager");
        Destroy(gamemanager);

        Level mainMenu = GameManager.FindInLevels("MainTitleScene");
        int mainMenuNumber = mainMenu.GetLevelNumber();
        GameManager.SwitchLevelTo(mainMenuNumber);
    }
}
