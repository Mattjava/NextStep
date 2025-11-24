using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "NextStep/Quest")]
public class QuestData : ScriptableObject {
    public string questId;
    public string questName;
    [TextArea] public string description;
    public int xpReward;

    // Generic parameters each quest type may use
    public int stepGoal;
    public int streakDays;
    public int timeLimitHour;

    public QuestType questType;

    // Loot rewards
    public List<LootReward> lootRewards = new List<LootReward>();
}

public enum QuestType {
    StepGoal,
    Streak,
    TimeWindow,
    Daily
}
