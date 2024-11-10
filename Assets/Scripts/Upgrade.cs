using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    private Dictionary<string, int> stats = new Dictionary<string, int>()
    {
        { "atkDamage", 0 },
        { "atkRange", 0 },
        { "atkKnockbackForce", 0 },
        { "atkCooldown", 0 },
        { "handling", 0 }
    };

    private Dictionary<string, int> statCosts = new Dictionary<string, int>()
    {
        { "atkDamage", 30 },
        { "atkRange", 20 },
        { "atkKnockbackForce", 5 },
        { "atkCooldown", 15 },
        { "handling", 10 }
    };

    public void SetStat(string statName, int value)
    {
        if (stats.ContainsKey(statName))
        {
            stats[statName] = value;
        }
        else
        {
            Debug.LogWarning($"Stat {statName} does not exist.");
        }
    }

    public int GetUpgradeCost()
    {
        int upgradeCost = 0;
        foreach (var stat in stats)
        {
            upgradeCost += statCosts[stat.Key] * Mathf.Abs(stat.Value);
        }
        return upgradeCost;
    }
    public Dictionary<string, int> getStats()
    {
        return stats;
    }
}
