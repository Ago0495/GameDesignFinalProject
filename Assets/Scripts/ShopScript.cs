using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI Pricetxt;
    public TextMeshProUGUI Quantitytxt;
    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        Pricetxt.text = "Price: $" + ShopManager.GetComponent<ShopManager>().items[2, ItemID].ToString();
        Quantitytxt.text = ShopManager.GetComponent<ShopManager>().items[2, ItemID].ToString();
    }

    public void DisplayRandomItems()
    {

    }
}
