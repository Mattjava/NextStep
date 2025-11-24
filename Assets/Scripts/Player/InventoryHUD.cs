using UnityEngine;
using TMPro;

public class InventoryHUD : MonoBehaviour {
    public PlayerInventory inventory;
    public TextMeshProUGUI text;

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
        if (inventory == null || text == null) return;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<b>Inventory:</b>");

        foreach (var entry in inventory.items) {
            sb.AppendLine($"• {entry.item.itemName} x{entry.quantity}");
        }

        text.text = sb.ToString();
    }
}
