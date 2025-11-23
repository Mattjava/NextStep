using UnityEngine;
using UnityEngine.UI;

public class QuestProgressUI : MonoBehaviour {
    public Slider progressSlider;

    private QuestInstance questInstance;

    public void Bind(QuestInstance instance) {
        questInstance = instance;

        if (progressSlider == null) {
            Debug.LogError("Progress Slider not assigned!");
            return;
        }

        progressSlider.maxValue = instance.data.stepGoal;
        UpdateUI();
    }

    public void UpdateUI() {
        if (questInstance == null) return;
        if (progressSlider == null) return;

        progressSlider.value = questInstance.currentSteps;
    }
}
