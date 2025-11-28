public class StepGoalQuest : Quest {
    private int startSteps;

    public StepGoalQuest(QuestInstance instance, int initialSteps)
        : base(instance) {
        startSteps = initialSteps;
    }

    public override void Initialize() { }

    public override void OnStepUpdate(int newTotalSteps) {
        int gained = newTotalSteps - startSteps;
        instance.currentSteps = gained;

        if (instance.currentSteps >= instance.data.stepGoal)
            instance.readyToComplete = true;
    }

    public override void Tick() { }
}
