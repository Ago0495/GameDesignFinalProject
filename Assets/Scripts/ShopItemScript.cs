using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShopItemScript : MonoBehaviour
{
    private int itemPrice;
    private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Upgrade upgrade;
    private Dictionary<string, int> upgradeTypes;


    // Start is called before the first frame update
    void Start()
    {
        upgrade = new Upgrade();   

        GenerateUpgrade();
        itemPrice = upgrade.GetUpgradeCost();

        DisplayItemInfo();
    }

    private void DisplayItemInfo()
    {
        itemPriceText.text = ("$" +  itemPrice).ToString();
    }

    private void GenerateUpgrade()
    {
        upgradeTypes = upgrade.GetStats();
        string randStat = pickRandomUpgradeStat();

        int randUpgradePower = Random.Range(1, 5);
        upgrade.SetStatPower(randStat, randUpgradePower);
    }

    private string pickRandomUpgradeStat()
    {
        List<string> statNameList = new List<string>(upgradeTypes.Keys);

        int rand = Random.Range(0, upgradeTypes.Count);
        return statNameList[rand];
    }
    public void BuyUpgrade()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerScript playerScript = playerObj.GetComponent<PlayerScript>();
        int playerCurrency = playerScript.GetCurrency();

        if (playerCurrency >= itemPrice)
        {
            playerScript.ChangeCurrency(-itemPrice);

            Button button = GetComponent<Button>();
            button.interactable = false;

            SetInheritenceColor(transform, button.colors.disabledColor);
        }
    }
    public void SetInheritenceColor(Transform element, Color color)
    {
        // Check if there's an Image component on the current element and set its alpha
        Image image = element.GetComponent<Image>();
        TextMeshProUGUI text = element.GetComponent<TextMeshProUGUI>();
        if (image != null)
        {
            image.color = color;
        }
        else if (text != null)
        {
            text.color = color;
        }

        // Recursively call this function for each child of the current element
        foreach (Transform child in element)
        {
            SetInheritenceColor(child, color);
        }
    }
}
