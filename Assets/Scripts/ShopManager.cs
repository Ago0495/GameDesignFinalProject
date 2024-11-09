using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[,] items = new int[5, 5];
    public int coins;
    public TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        txt.text = "Coins: " + coins + coins.ToString();

        //ID
        items[1, 1] = 1;
        items[1, 2] = 2;
        items[1, 3] = 3;
        items[1, 4] = 4;

        //Price
        items[2, 1] = 10;
        items[2, 2] = 20;
        items[2, 3] = 30;
        items[2, 4] = 40;

        //quantity
        items[3, 1] = 0;
        items[3, 2] = 0;
        items[3, 3] = 0;
        items[3, 4] = 0;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (coins >= items[2, ButtonRef.GetComponent<ShopScript>().ItemID])
        {
            coins -= items[2, ButtonRef.GetComponent<ShopScript>().ItemID];

            items[3, ButtonRef.GetComponent<ShopScript>().ItemID]++;

            txt.text = "Coins: " + coins.ToString();
            ButtonRef.GetComponent<ShopScript>().Quantitytxt.text = items[3, ButtonRef.GetComponent<ShopScript>().ItemID].ToString();
        }
    }
}
