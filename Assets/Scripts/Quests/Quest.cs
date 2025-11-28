public abstract class Quest {
    protected QuestInstance instance;
    public QuestInstance Instance => instance;

    public Quest(QuestInstance instance) {
        this.instance = instance;
    }

    public abstract void Initialize();
    public abstract void OnStepUpdate(int newTotalSteps);
    public abstract void Tick();
}
