using System;
using UnityEngine;

[Serializable]
public class QuestInstance {
    public QuestData data;
    public QuestState state;

    public int currentSteps;
    public int currentStreak;
    public DateTime startTime;
    public float timeLimitSeconds;
    public float elapsedSeconds;

    public float TimeRemaining => Mathf.Max(0, timeLimitSeconds - elapsedSeconds);

    //  Progress for UI (0–1)
    public float Progress { get; private set; }
    public void SetProgress(float value) {
        Progress = value;
    }
    public bool IsCompleted => state == QuestState.Completed;
    public bool IsActive => state == QuestState.Active;

    public QuestInstance(QuestData data) {
        this.data = data;
        state = QuestState.Active;
        startTime = DateTime.Now;
        Progress = 0f;
    }

    // =====================================================
    //  UPDATE STEP PROGRESS (called from Quest.OnStepUpdate)
    // =====================================================
    public void ApplyStepUpdate(int totalStepsToday, int startingSteps) {
        if (IsCompleted || !IsActive) return;

        int stepsSinceStart = Mathf.Max(0, totalStepsToday - startingSteps);

        currentSteps = stepsSinceStart;

        if (data.questType == QuestType.StepGoal) {
            if (data.stepGoal > 0)
                Progress = Mathf.Clamp01((float)currentSteps / data.stepGoal);
            else
                Progress = 0f;
        }
 
    }

    // =====================================================
    //  DETERMINE IF QUEST IS COMPLETE
    // =====================================================
    public bool IsComplete() {
        if (IsCompleted) return true;

        switch (data.questType) {
            case QuestType.StepGoal:
                return currentSteps >= data.stepGoal;

            case QuestType.Streak:
                return currentStreak >= data.streakDays;

            case QuestType.TimeWindow:
                return (DateTime.Now - startTime).TotalHours <= data.timeLimitHour &&
                       currentSteps >= data.stepGoal;

            case QuestType.Daily:
                // Daily auto-complete on first progress
                return currentSteps > 0;

            default:
                return false;
        }
    }

    // =====================================================
    //  COMPLETE / FAIL
    // =====================================================
    public void Complete() {
        state = QuestState.Completed;
    }

    public void Fail() {
        state = QuestState.Failed;
    }
}
