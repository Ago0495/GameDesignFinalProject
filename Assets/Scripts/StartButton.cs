using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnClick()
    {
        Level shopLevel = GameManager.FindInLevels("ShopLevel");
        int shopLevelNumber = shopLevel.GetLevelNumber();
        if (GameManager.GetGameStatus())
        {
            GameManager.SetGameCompleted(false);
            Time.timeScale = 1f;
            PlayerScript.GetPlayerInstance().Activate();
            PlayerScript.GetPlayerInstance().TakeDamage(99999);
        }
        
        GameManager.SwitchLevelTo(shopLevelNumber);
    }
}
