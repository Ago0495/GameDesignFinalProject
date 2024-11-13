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
    [SerializeField] private TextMeshProUGUI rerollButtonText;
    [SerializeField] private GameObject[] sellWeapons;
    [SerializeField] private int maxPower;
    private Canvas shopCanvas;
    private int rerollPrice;
    private int numReroll;


    private void Start()
    {
        shopCanvas = GetComponentInChildren<Canvas>();
        shopGridObject = GetComponentInChildren<GridLayoutGroup>().gameObject;
        shopCanvas.enabled = false;
        rerollPrice = 10;
        numReroll = 0;

        ClearShop();
        GenerateRandomItems();
        DisplayReroll();
    }

    public void GenerateRandomItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject itemObj = Instantiate(shopItemPrefab, shopGridObject.transform);
            itemObj.GetComponent<ShopItemScript>().SetWeapons(sellWeapons);
            itemObj.GetComponent<ShopItemScript>().SetMaxPower(maxPower);
        }
    }

    public void ClearShop()
    {
        foreach (Transform item in shopGridObject.transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void RefreshShop()
    {
        ClearShop();
        GenerateRandomItems();
    }

    public void RerollShop()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerScript playerScript = playerObj.GetComponentInParent<PlayerScript>();
        int playerCurrency = playerScript.GetCurrency();

        if (playerCurrency >= rerollPrice)
        {
            playerScript.ChangeCurrency(-rerollPrice);

            RefreshShop();
            numReroll++;

            rerollPrice += rerollPrice * (int)(numReroll * 0.25f);
        }

        DisplayReroll();
    }

    private void DisplayReroll()
    {
        if (rerollButtonText != null)
        {
            rerollButtonText.text = ("Reroll\n\n" + "<sprite=75>" + rerollPrice);
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
