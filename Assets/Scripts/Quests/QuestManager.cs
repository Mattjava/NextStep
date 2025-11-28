using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public List<QuestData> availableQuests;

    private List<Quest> activeQuests = new();
    private int playerStartingSteps;
    public static int StartingSteps { get; private set; }
    public QuestProgressUI stepGoalUI;

    public QuestCompletionHandler completionHandler;

    private void Start() {
        // 1. Get starting steps
        #if UNITY_EDITOR
                playerStartingSteps = 0;   // Always start at zero for demo mode
        #else
        if (FitBridgeWrapper.Instance != null)
            playerStartingSteps = FitBridgeWrapper.Instance.stepCount;
        else
            playerStartingSteps = 0;
        #endif


        // 2. Load quests
        LoadQuests();

        // 3. Subscribe to live updates
        if (FitBridgeWrapper.Instance != null)
            FitBridgeWrapper.Instance.OnStepUpdate += OnStepUpdate;
    }
    private void Update() {
        foreach (var quest in activeQuests)
            quest.Tick();
    }

    private void OnDestroy() {
        if (FitBridgeWrapper.Instance != null)
            FitBridgeWrapper.Instance.OnStepUpdate -= OnStepUpdate;
    }

    public void OnStepUpdate(int newTotalSteps) {
        foreach (var quest in activeQuests) {
            quest.OnStepUpdate(newTotalSteps);

            if (!quest.Instance.IsCompleted) {
                if (quest.Instance.readyToComplete)
                    CompleteQuest(quest);

                if (quest.Instance.readyToFail)
                    FailQuest(quest);
            }
        }

        stepGoalUI?.UpdateUI();
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

    // COMPLETION LOGIC
    private void CompleteQuest(Quest quest) {
        Debug.Log("[QUEST MANAGER] CompleteQuest() called");

        quest.Instance.Complete();   // complete the quest

        Debug.Log($"Quest completed: {quest.Instance.data.questName}");

        if (completionHandler != null)
            completionHandler.CompleteQuest(quest.Instance.data);

        if (stepGoalUI != null)
            stepGoalUI.UpdateUI();
    }
    public List<Quest> GetActiveQuests() {
        return activeQuests; 
    }

    private void FailQuest(Quest quest) {
        quest.Instance.Fail();
        Debug.Log($"Quest failed: {quest.Instance.data.questName}");
    }



}
