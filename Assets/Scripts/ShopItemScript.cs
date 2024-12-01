using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class ShopItemScript : MonoBehaviour
{
    private int itemPrice;
    private string itemName;
    private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI itemStatNameText;
    [SerializeField] private TextMeshProUGUI itemStatPowerText;
    [SerializeField] private Upgrade upgrade;
    [SerializeField] private Color[] rarityColors = new Color[5];
    private GameObject[] sellWeapons;
    private int maxPower;
    private Dictionary<string, int> upgradeTypes;

    private UpgradeShopItem upgradeItem;
    private WeaponShopItem weaponItem;


    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomShopItem();
    }

    private void DisplayItemInfo(string name, string power, string price, Color color)
    {
        itemStatNameText.text = name;
        itemStatPowerText.text = power;
        itemPriceText.text = ("<sprite=75>" +  price).ToString();

        var border = transform.Find("border");
        var borderImage = border.GetComponent<Image>();
        borderImage.color = color;
    }

    private void GenerateRandomShopItem()
    {
        int rand = Random.Range(1, 100);
        if (rand <= 25)
        {
            weaponItem = new WeaponShopItem(sellWeapons);
            GameObject weaponObj = weaponItem.GetWeapon();
            upgrade = weaponObj.GetComponent<WeaponScript>().GetBaseStats();

            itemPrice = weaponItem.GetPrice();

            int weaponLvl = weaponObj.GetComponent<WeaponScript>().GetComponent<WeaponScript>().GetLvl();

            string weaponName = weaponObj.tag;
            string weaponLvlText = "lvl." + weaponLvl.ToString();
            string weaponText = weaponName + "\n" + weaponLvlText;

            DisplayItemInfo("WPN", weaponText, itemPrice.ToString(), rarityColors[weaponLvl]);
        }
        else
        {
            upgradeItem = new UpgradeShopItem(maxPower);
            upgrade = upgradeItem.GetUpgrade();

            itemPrice = upgradeItem.GetPrice();
            itemName = upgradeItem.GetStatName();

            string statText = "";

            if (upgradeItem.GetStatPower() >= 0)
            {
                statText += "+" + upgradeItem.GetStatPower();
            }
            else
            {
                statText += upgradeItem.GetStatPower();
            }

            statText += "\n" + upgradeItem.GetStatName().Replace("atk", "");

            DisplayItemInfo("UPG", statText, itemPrice.ToString(), rarityColors[Mathf.Abs(upgrade.GetStatPower(upgradeItem.GetStatName())) - 1]);
        }
    }

    private List<Stat> FindPopulatedStats()
    {
        List<Stat> stats = new List<Stat>();

        foreach (Stat stat in upgrade.GetStatsList())
        {
            if (stat.GetStatPower() != 0)
            {
                stats.Add(stat);
            }
        }

        return stats;
    }

    public void BuyItem()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerScript playerScript = playerObj.GetComponentInParent<PlayerScript>();
        Transform playerWeapon = playerScript.GetCurrentWeapon();
        WeaponScript playerWeaponScript = null;

        if (playerWeapon != null)
        {
            playerWeaponScript = playerWeapon.GetComponent<WeaponScript>();
        }

        int playerCurrency = playerScript.GetCurrency();

        if (playerCurrency >= itemPrice)
        {

            Button button = GetComponent<Button>();

            if (upgradeItem != null && playerWeapon != null)
            {
                if(upgradeItem.GetStatName() == "Health" && playerScript.GetCurrentHP() != playerScript.GetMaxHP()) 
                {
                    playerScript.ChangeCurrency(-itemPrice);
                    playerScript.TakeDamage(-upgradeItem.GetStatPower());
                }
                if (playerWeaponScript.GetLvl() > playerWeaponScript.GetNumUpgrades())
                {
                    playerScript.ChangeCurrency(-itemPrice);
                    button.interactable = false;

                    playerWeapon = playerScript.GetCurrentWeapon();
                    playerWeaponScript = playerWeapon.GetComponent<WeaponScript>();
                    playerWeaponScript.AddUpgrade(upgrade);

                    SetBuyColor(transform);
                }
            }
            if (weaponItem != null && playerScript.GetMaxWeapons() > playerScript.GetNumWeapons())
            {
                playerScript.ChangeCurrency(-itemPrice);
                button.interactable = false;

                playerScript.AddWeaponToEntity(weaponItem.GetWeapon());

                SetBuyColor(transform);
            }

        }
    }

    public void SetWeapons(GameObject[] weapons)
    {
        sellWeapons = weapons;
    }

    public void SetMaxPower(int power)
    {
        maxPower = power;
    }
    public void SetBuyColor(Transform element)
    {
        // Check if there's an Image component on the current element and set its alpha
        Image image = element.GetComponent<Image>();
        TextMeshProUGUI text = element.GetComponent<TextMeshProUGUI>();
        if (image != null)
        {
            image.color -= new Color(0.3f, 0.3f, 0.3f, 0.3f);
        }
        else if (text != null)
        {
            text.color -= new Color(0.3f, 0.3f, 0.3f, 0.3f);
        }

        // Recursively call this function for each child of the current element
        foreach (Transform child in element)
        {
            SetBuyColor(child);
        }
    }
}
