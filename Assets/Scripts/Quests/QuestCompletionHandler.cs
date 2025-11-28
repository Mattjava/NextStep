using UnityEngine;

public class QuestCompletionHandler : MonoBehaviour {
    public PlayerInventory inventory;
    public PlayerStats playerStats;
    public QuestCompletePopup popup;

    public void CompleteQuest(QuestData quest) {
        playerStats.AddXP(quest.xpReward);

        string lootSummary = "";
        foreach (var reward in quest.lootRewards) {
            inventory.AddItem(reward.lootItem, reward.quantity);
            lootSummary += $"\n • {reward.quantity}x {reward.lootItem.name}";
        }

        // Build popup message
        string msg = $"QUEST COMPLETE!\n{quest.questName}\n+{quest.xpReward} XP{lootSummary}";

        // Show popup
        if (popup != null)
            popup.Show(msg);

        Debug.Log($"Quest complete: {quest.questName}");
    }

}
