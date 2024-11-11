using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyAmountText;
    [SerializeField] private GameObject itemIcon;
    private PlayerScript playerScript;

    public void Start()
    {
        playerScript = GetComponentInParent<PlayerScript>();
    }

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        currencyAmountText.text = "<sprite=75>" + playerScript.GetCurrency().ToString();
        var iconImage = itemIcon.GetComponent<Image>();
        iconImage.sprite = playerScript.GetCurrentWeapon().GetComponent<SpriteRenderer>().sprite;
        iconImage.SetNativeSize();
    }
}
