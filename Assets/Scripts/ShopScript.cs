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
    [SerializeField] private Canvas shopCanvas;

    private void Start()
    {
        shopCanvas = GetComponentInChildren<Canvas>();
        shopCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Pricetxt.text = "Price: $" + ShopManager.GetComponent<ShopManager>().items[2, ItemID].ToString();
        //Quantitytxt.text = ShopManager.GetComponent<ShopManager>().items[2, ItemID].ToString();
    }

    public void DisplayRandomItems()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shopCanvas.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shopCanvas.enabled = false;
        }
    }
}
