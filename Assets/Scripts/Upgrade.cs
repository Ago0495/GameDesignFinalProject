using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[Serializable]
public class Upgrade
{
    [SerializeField] List<Stat> upgradeStats;

    public Upgrade()
    {
        upgradeStats = new List<Stat>();

        upgradeStats.Add(new Stat("atkDamage", 0, 30));
        upgradeStats.Add(new Stat("atkRange", 0, 20));
        upgradeStats.Add(new Stat("atkKnockback", 0, 5));
        upgradeStats.Add(new Stat("atkCooldown", 0, 15));
        upgradeStats.Add(new Stat("handling", 0, 10));
    }


    public Dictionary<string, int> ToDictionary()
    {
        Dictionary<string, int> newDict = new Dictionary<string, int>();

        foreach (Stat item in upgradeStats)
        {
            newDict.Add(item.GetStatName(), item.GetStatPower());
        }

        return newDict;
    }

    public void SetStatPower(string statName, int value)
    {
        // Find the Stat in upgradeStats with the matching statName and set its power
        foreach (Stat stat in upgradeStats)
        {
            if (stat.GetStatName() == statName)
            {
                stat.SetStatPower(value);  // Set the power directly on the Stat object
                return;  // Exit the function once we've updated the stat
            }
        }
        // Log a warning if no stat with the given name was found
        Debug.LogWarning($"Stat {statName} does not exist.");
    }


    public int GetStatPower(string statName)
    {
        var stats = ToDictionary();

       return stats[statName];
    }

    public int GetUpgradeCost()
    {
        int upgradeCost = 0;

        foreach (Stat item in upgradeStats)
        {
            upgradeCost += item.GetStatCost() * item.GetStatPower() * item.GetStatPower();
        }

        return upgradeCost;
    }
    public Dictionary<string, int> GetStats()
    {
        return ToDictionary();
    }

    public List<Stat> GetStatsList()
    {
        return upgradeStats;
    }
}

[Serializable]
public class Stat
{
    /*[SerializeField]*/ string statName;
    [SerializeField] int statPower;
    [SerializeField] int statCost;

    public Stat(string statName, int statPower, int statCost)
    {
        this.statName = statName;
        this.statPower = statPower;
        this.statCost = statCost;
    }

    public string GetStatName()
    {
        return statName;
    }
    public int GetStatPower()
    {
        return statPower;
    }
    public int GetStatCost()
    {
        return statCost;
    }

    public void SetStatName(string name)
    {
        statName = name;
    }
    public void SetStatPower(int power)
    {
        statPower = power;
    }
    public void SetStatCost(int cost)
    {
        statCost = cost;
    }

    public static Stat operator +(Stat stat1, Stat stat2)
    {
        return new Stat(stat1.statName, stat1.statPower + stat2.statPower, stat1.statCost);
    }
}
