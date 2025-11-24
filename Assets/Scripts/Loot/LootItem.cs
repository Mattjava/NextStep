using UnityEngine;

[CreateAssetMenu(fileName = "LootItem", menuName = "NextStep/Loot Item")]
public class LootItem : ScriptableObject {
    public string itemId;            // “ember_core”
    public string itemName;          // “Ember Core”
    public Sprite icon;              // Item icon if you want it
    public LootRarity rarity;        // NEW
    public string description;       // Flavor
}
