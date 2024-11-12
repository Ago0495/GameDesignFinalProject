using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopItem
{
    private Upgrade upgrade;
    private Dictionary<string, int> upgradeTypes;
    private int itemPrice;
    private Color rarityColor;
    private string randStat;

    public UpgradeShopItem(int maxPower)
    {
        upgrade = new Upgrade();
        upgradeTypes = upgrade.GetStats();
        randStat = PickRandomUpgradeStat();

        int randUpgradePower = Random.Range(1, maxPower + 1);
        if (randStat == "atkCooldown")
        {
            randUpgradePower *= -1;
        }

        upgrade.SetStatPower(randStat, randUpgradePower);
        itemPrice = upgrade.GetUpgradeCost();
    }

    private string PickRandomUpgradeStat()
    {
        List<string> statNameList = new List<string>(upgradeTypes.Keys);
        int rand = Random.Range(0, upgradeTypes.Count);
        return statNameList[rand];
    }

    public string GetStatName()
    {
        return randStat;
    }

    public int GetStatPower()
    {
        return upgrade.GetStatPower(GetStatName());
    }

    public Upgrade GetUpgrade()
    {
        return upgrade;
    }

    public int GetPrice()
    {
        return itemPrice;
    }
}
