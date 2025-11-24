using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryGridHUD : MonoBehaviour {
    public PlayerInventory inventory;
    public GameObject slotPrefab;
    public Transform gridParent;

    private void OnEnable() {
        if (inventory != null)
            inventory.OnInventoryChanged += RefreshHUD;
    }

    private void OnDisable() {
        if (inventory != null)
            inventory.OnInventoryChanged -= RefreshHUD;
    }

    private void Start() {
        RefreshHUD();
    }

    private void RefreshHUD() {
        // Clear old slots
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        // Add one slot per item
        foreach (var entry in inventory.items) {
            var slot = Instantiate(slotPrefab, gridParent);

            // icon
            var icon = slot.GetComponent<Image>();
            if (icon != null)
                icon.sprite = entry.item.icon;

            // item name text
            var nameText = slot.transform.GetComponentInChildren<TextMeshProUGUI>();
            nameText.text = $"{entry.item.itemName} x{entry.quantity}";

            // rarity color
            nameText.color = RarityColors.GetColor(entry.item.rarity);
        }
    }
}
