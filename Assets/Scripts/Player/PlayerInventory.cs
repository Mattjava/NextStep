using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    [Serializable]
    public class InventoryEntry {
        public LootItem item;
        public int quantity;
    }

    public List<InventoryEntry> items = new List<InventoryEntry>();

    public event Action OnInventoryChanged;

    public void AddItem(LootItem item, int amount) {
        if (item == null || amount <= 0) return;

        var entry = items.Find(e => e.item == item);

        if (entry != null) {
            entry.quantity += amount;
        } else {
            items.Add(new InventoryEntry {
                item = item,
                quantity = amount
            });
        }

        Debug.Log($"Item Added {amount}x {item.itemName} ({item.rarity})");
        OnInventoryChanged?.Invoke();
    }

    public bool RemoveItem(LootItem item, int amount) {
        var entry = items.Find(e => e.item == item);
        if (entry == null || entry.quantity < amount) return false;

        entry.quantity -= amount;

        if (entry.quantity <= 0)
            items.Remove(entry);

        OnInventoryChanged?.Invoke();
        return true;
    }

    public int GetQuantity(LootItem item) {
        var entry = items.Find(e => e.item == item);
        return entry != null ? entry.quantity : 0;
    }

    public bool HasItem(LootItem item, int amount = 1) {
        return GetQuantity(item) >= amount;
    }
}
