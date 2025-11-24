using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class ActiveQuestHUD : MonoBehaviour {
    public QuestManager questManager;
    public TextMeshProUGUI text;

    private void Update() {
        RefreshHUD();
    }

    private void RefreshHUD() {
        if (questManager == null || text == null) return;

        List<Quest> quests = questManager.GetActiveQuests();
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<b>Active Quests:</b>");

        foreach (var quest in quests) {
            var inst = quest.Instance;
            var data = inst.data;

            // Only StepGoal for now
            if (data.questType == QuestType.StepGoal) {
                sb.AppendLine(
                    $"{data.questName}: {inst.currentSteps}/{data.stepGoal}"
                );
            } else if (data.questType == QuestType.TimeWindow) {
                // Format mm:ss
                int seconds = Mathf.CeilToInt(inst.TimeRemaining);
                int minutes = seconds / 60;
                int secs = seconds % 60;

                sb.AppendLine($"{data.questName}: {inst.currentSteps}/{data.stepGoal}  ({minutes:D2}:{secs:D2})");
            } else {
                sb.AppendLine($"{data.questName}: In progress");
            }
        }

        text.text = sb.ToString();
    }
}
