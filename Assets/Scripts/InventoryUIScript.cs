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
    [SerializeField] private Slider hpBar;
    [SerializeField] private Image GameOverScrene;

    public void Start()
    {
        inventorySlots = new List<GameObject>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponentInParent<PlayerScript>();

        inventoryGridObj = GetComponentInChildren<GridLayoutGroup>().gameObject;

        hpBar.maxValue = playerScript.GetMaxHP();
    }

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        currencyAmountText.text = "<sprite=75>" + playerScript.GetCurrency().ToString();
        hpBar.value = playerScript.GetCurrentHP();
        if (playerScript.GetCurrentHP() <= 0)
        {
            GameOverScrene.gameObject.SetActive(true);
        }
        int numWeapons = playerScript.GetNumWeapons();
        int index = playerScript.GetCurrentWeaponIndex();
        

        for (int i = 0; i < numWeapons; i++)
        {
            if (i >= inventorySlots.Count)
            {
                while (i >= inventorySlots.Count)
                {
                    CreateSlot();
                }
            }
            if (inventorySlots.Count % 2 == 0)
            {
                CreateSlot();
            }
            //change slot visual here
            GameObject weapon = playerScript.GetStachedWeapon(index + i - inventorySlots.Count/2).gameObject;
            Image iconImage = inventorySlots[i].transform.GetChild(0).GetComponent<Image>();
            iconImage.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

            //Fade color
            float distance = Mathf.Abs(i - (inventorySlots.Count / 2));
            float fadeValue = Mathf.Clamp01(1.0f - (distance / (numWeapons / 1.0f))); //Fade rate

            iconImage.color = new Color(fadeValue, fadeValue, fadeValue, fadeValue);

            iconImage.SetNativeSize();
        }
    }

    private void CreateSlot()
    {
        int index = playerScript.GetCurrentWeaponIndex();
        GameObject newSlot = Instantiate(slotPrefab, inventoryGridObj.transform);
        inventorySlots.Add(newSlot);
    }
}
