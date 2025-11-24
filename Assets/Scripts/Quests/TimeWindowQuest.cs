using UnityEngine;

public class TimeWindowQuest : Quest {
    private int startingSteps;
    private float timeLimitSeconds;
    private float elapsedTime;

    public TimeWindowQuest(QuestInstance instance) : base(instance) {
    }

    public override void Initialize() {
        Instance.timeLimitSeconds = Instance.data.timeLimitHour * 3600f;
        Instance.elapsedSeconds = 0f;


        startingSteps = QuestManager.StartingSteps;

    }

    public override void OnStepUpdate(int totalStepsToday) {
        // Only track progress if active
        if (!Instance.IsActive || Instance.IsCompleted)
            return;

        UpdateProgress(totalStepsToday);
    }

    public override void Tick() {
        if (!Instance.IsActive || Instance.IsCompleted)
            return;

        Instance.elapsedSeconds += Time.deltaTime;


        if (elapsedTime >= timeLimitSeconds) {
            Fail();    // call base.Fail()
        } else if (Instance.IsComplete()) {
            Complete();  // call base.Complete()
        }
    }

    private void UpdateProgress(int totalStepsToday) {
        int stepsSinceStart = Mathf.Max(0, totalStepsToday - startingSteps);
        Instance.currentSteps = stepsSinceStart;

        if (Instance.data.stepGoal > 0) {
            float newProg = Mathf.Clamp01(
                (float)Instance.currentSteps / Instance.data.stepGoal
            );
            Instance.SetProgress(newProg);
        }

    }
}
