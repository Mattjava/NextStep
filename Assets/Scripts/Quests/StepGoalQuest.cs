public class StepGoalQuest : Quest {
    private int startSteps;

    public StepGoalQuest(QuestInstance instance, int initialSteps) : base(instance) {
        startSteps = initialSteps;
    }

    public override void Initialize() {
        // Nothing special needed
    }

    public override void OnStepUpdate(int newTotalSteps) {
        int gained = newTotalSteps - startSteps;
        instance.currentSteps = gained;

        if (gained >= instance.data.stepGoal)
            Complete();
    }

    public override void Tick() { }
}
