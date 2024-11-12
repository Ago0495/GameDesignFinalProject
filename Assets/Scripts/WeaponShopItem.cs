using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopItem
{
    private GameObject selectedWeapon;
    private int itemPrice;

    public WeaponShopItem(GameObject[] weapons)
    {
        SelectRandomWeapon(weapons);
    }

    private void SelectRandomWeapon(GameObject[] weapons)
    {
        int numWeapons = weapons.Length;
        float randomVal = Random.Range(0.0f, 1.0f);
        float exponentFactor = 3.0f;
        int index = Mathf.FloorToInt(Mathf.Pow(randomVal, exponentFactor) * numWeapons);
        index = Mathf.Clamp(index, 0, numWeapons - 1);

        selectedWeapon = weapons[index];
        Upgrade weaponStats = selectedWeapon.GetComponent<WeaponScript>().GetBaseStats();
        itemPrice = weaponStats.GetUpgradeCost() / 20;
    }

    public GameObject GetWeapon()
    {
        return selectedWeapon;
    }

    public int GetPrice()
    {
        return itemPrice;
    }
}
