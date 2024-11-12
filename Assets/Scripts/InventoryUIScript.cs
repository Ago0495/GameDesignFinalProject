using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyAmountText;
    [SerializeField] private GameObject inventoryGridObj;
    [SerializeField] private GameObject slotPrefab;
    private PlayerScript playerScript;
    [SerializeField] private List<GameObject> inventorySlots;

    public void Start()
    {
        inventorySlots = new List<GameObject>();
        playerScript = GetComponentInParent<PlayerScript>();
        inventoryGridObj = GetComponentInChildren<GridLayoutGroup>().gameObject;
    }

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        currencyAmountText.text = "<sprite=75>" + playerScript.GetCurrency().ToString();

        int numWeapons = playerScript.GetNumWeapons();
        for (int i = 0; i < numWeapons; i++)
        {
            if (i >= inventorySlots.Count)
            {
                while (i >= inventorySlots.Count)
                {
                    CreateSlot();
                }
            }
            int index = playerScript.GetCurrentWeaponIndex();
            GameObject weapon = playerScript.GetStachedWeapon(index + i).gameObject;
            Image iconImage = inventorySlots[i].transform.GetChild(0).GetComponent<Image>();
            iconImage.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
            
            float val = (3 - i) / 3f;
            iconImage.color = new Color(val, val, val, val);

            iconImage.SetNativeSize();
        }
    }

    private void CreateSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryGridObj.transform);
        inventorySlots.Add(newSlot);
    }
}
