using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void OnClick()
    {
        Level mainMenu = GameManager.FindInLevels("MainTitleScene");
        int mainMenuNumber = mainMenu.GetLevelNumber();
        Time.timeScale = 1f;
        PlayerScript.GetPlayerInstance().TakeDamage(99999);
        GameManager.SwitchLevelTo(mainMenuNumber);
    }
}
