using UnityEngine;

public class QuestCompletionHandler : MonoBehaviour {
    public PlayerInventory inventory;
    public PlayerStats playerStats;

    public void CompleteQuest(QuestData quest) {
        playerStats.AddXP(quest.xpReward);

        foreach (var reward in quest.lootRewards) {
            inventory.AddItem(reward.lootItem, reward.quantity);
        }

        Debug.Log($"Quest complete: {quest.questName}");
    }
}
