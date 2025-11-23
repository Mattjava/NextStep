using System;

[Serializable]
public class QuestInstance {
    public QuestData data;
    public QuestState state;
    public int currentSteps;
    public int currentStreak;
    public DateTime startTime;

    public bool IsCompleted => state == QuestState.Completed;
    public bool IsActive => state == QuestState.Active;

    public QuestInstance(QuestData data) {
        this.data = data;
        state = QuestState.Active;
        startTime = DateTime.Now;
    }

    public void Complete() {
        state = QuestState.Completed;
      //  Debug.Log($"Quest complete: {data.questName}");
    }

    public void Fail() {
        state = QuestState.Failed;
    //    Debug.Log($"Quest failed: {data.questName}");
    }
}
