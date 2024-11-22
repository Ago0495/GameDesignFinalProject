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
        GameManager.SwitchLevelTo(shopLevelNumber);
    }
}
