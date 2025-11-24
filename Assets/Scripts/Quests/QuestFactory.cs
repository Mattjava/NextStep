public static class QuestFactory {
    public static Quest Create(QuestInstance instance, int playerStartingSteps) {
        return instance.data.questType switch {
            QuestType.StepGoal => new StepGoalQuest(instance, playerStartingSteps),
            QuestType.Streak => new StreakQuest(instance),
            QuestType.TimeWindow => new TimeWindowQuest(instance),
            // default fallback
            _ => new StepGoalQuest(instance, playerStartingSteps)
        };
    }
}
