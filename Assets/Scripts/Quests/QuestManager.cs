using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public List<QuestData> availableQuests;

    private List<Quest> activeQuests = new();
    private int playerStartingSteps;

    public QuestProgressUI stepGoalUI;

    private void Start() {
        // 1. Get starting steps
        if (FitBridgeWrapper.Instance != null)
            playerStartingSteps = FitBridgeWrapper.Instance.stepCount;
        else
            playerStartingSteps = 0;

        // 2. Load quests
        LoadQuests();

        // 3. Subscribe to live updates
        if (FitBridgeWrapper.Instance != null)
            FitBridgeWrapper.Instance.OnStepUpdate += OnStepUpdate;
    }

    private void OnDestroy() {
        if (FitBridgeWrapper.Instance != null)
            FitBridgeWrapper.Instance.OnStepUpdate -= OnStepUpdate;
    }

    public void OnStepUpdate(int newTotalSteps) {
        foreach (var quest in activeQuests)
            quest.OnStepUpdate(newTotalSteps);

        //  Refresh the progress bar
        if (stepGoalUI != null)
            stepGoalUI.UpdateUI();
    }


    private void LoadQuests() {
        activeQuests.Clear();

        foreach (var questData in availableQuests) {
            QuestInstance instance = new QuestInstance(questData);
            Quest quest = QuestFactory.Create(instance, playerStartingSteps);
            quest.Initialize();
            activeQuests.Add(quest);
        }

        var stepGoalQuest = activeQuests
            .Find(q => q.Instance.data.questType == QuestType.StepGoal);

        if (stepGoalQuest != null && stepGoalUI != null)
            stepGoalUI.Bind(stepGoalQuest.Instance);
        Debug.Log("Loaded quests: " + activeQuests.Count);
        Debug.Log("StepGoal quest: " + stepGoalQuest?.Instance.data.questName);

    }


}
