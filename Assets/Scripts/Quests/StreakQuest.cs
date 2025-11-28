public class StreakQuest : Quest {
    public StreakQuest(QuestInstance instance) : base(instance) { }

    public override void Initialize() {
        instance.currentStreak = 0;
    }

    public override void OnStepUpdate(int newStepsToday) {
        if (newStepsToday >= instance.data.stepGoal)
            instance.currentStreak++;
        else
            instance.currentStreak = 0;

        if (instance.currentStreak >= instance.data.streakDays)
            instance.readyToComplete = true;
    }

    public override void Tick() { }
}
