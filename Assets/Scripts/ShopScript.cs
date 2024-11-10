using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject shopGridObject;
    private Canvas shopCanvas;


    private void Start()
    {
        shopCanvas = GetComponentInChildren<Canvas>();
        shopGridObject = GetComponentInChildren<GridLayoutGroup>().gameObject;
        shopCanvas.enabled = false;
        GenerateRandomItems();
    }

    // Update is called once per frame
    void Update()
    {
        //Pricetxt.text = "Price: $" + ShopManager.GetComponent<ShopManager>().items[2, ItemID].ToString();
        //Quantitytxt.text = ShopManager.GetComponent<ShopManager>().items[2, ItemID].ToString();
    }

    public void GenerateRandomItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject itemObj = Instantiate(shopItemPrefab, shopGridObject.transform);
        }
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
