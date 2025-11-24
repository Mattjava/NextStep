using UnityEngine;

public static class RarityColors {
    public static Color GetColor(LootRarity rarity) {
        switch (rarity) {
            case LootRarity.Common:
                return new Color(0.75f, 0.75f, 0.75f); // Light gray

            case LootRarity.Uncommon:
                return new Color(0.35f, 0.85f, 0.40f); // Green

            case LootRarity.Rare:
                return new Color(0.30f, 0.55f, 1f); // Blue

            case LootRarity.Epic:
                return new Color(0.65f, 0.25f, 0.90f); // Purple

            default:
                return Color.white;
        }
    }
}
