using UnityEngine;

public class TimeWindowQuest : Quest {
    private int startingSteps;
    private float timeLimitSeconds;
    private float elapsedTime;

    public TimeWindowQuest(QuestInstance instance) : base(instance) { }

    public override void Initialize() {
        instance.timeLimitSeconds = instance.data.timeLimitHour * 3600f;
        instance.elapsedSeconds = 0f;

        startingSteps = QuestManager.StartingSteps;
    }

    public override void OnStepUpdate(int totalStepsToday) {
        if (!instance.IsActive || instance.IsCompleted)
            return;

        int stepsSinceStart = Mathf.Max(0, totalStepsToday - startingSteps);
        instance.currentSteps = stepsSinceStart;

        float newProg = Mathf.Clamp01(
            (float)instance.currentSteps / instance.data.stepGoal
        );
        instance.SetProgress(newProg);

        if (instance.currentSteps >= instance.data.stepGoal)
            instance.readyToComplete = true;
    }

    public override void Tick() {
        if (!instance.IsActive || instance.IsCompleted)
            return;

        instance.elapsedSeconds += Time.deltaTime;

        if (instance.elapsedSeconds >= instance.timeLimitSeconds)
            instance.readyToFail = true;
    }
}
