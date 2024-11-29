using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void OnClick()
    {
        Level mainMenu = GameManager.FindInLevels("MainTitleScene");
        int mainMenuNumber = mainMenu.GetLevelNumber();
        GameManager.SwitchLevelTo(mainMenuNumber);
    }
}
