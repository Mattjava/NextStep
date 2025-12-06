using UnityEngine;
using TMPro;

public class StepXPManager : MonoBehaviour {
    public int currentXP = 0;
    public int level = 1;
    public int stepsToday = 0;

    public int stepToXP = 10;

    public TextMeshProUGUI stepText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;

    void OnEnable() {
        if (FitBridgeWrapper.Instance != null)
            FitBridgeWrapper.Instance.OnStepUpdate += HandleStepUpdate;
    }

    void OnDisable() {
        if (FitBridgeWrapper.Instance != null)
            FitBridgeWrapper.Instance.OnStepUpdate -= HandleStepUpdate;
    }

    void HandleStepUpdate(int newSteps) {
        int gained = newSteps - stepsToday;

        if (gained > 0)
            AddXP(gained / stepToXP);

        stepsToday = newSteps;
        UpdateUI();
    }
    public void AddSteps(int amount) {
        stepsToday += amount;
        AddXP(amount / stepToXP);
        UpdateUI();
    }

    public void AddXP(int amount) {
        currentXP += amount;

        while (currentXP >= xpToNextLevel) {
            currentXP -= xpToNextLevel;
            level++;
        }

        UpdateUI();
    }

    int xpToNextLevel => (100 * (level * level)) + (50 * level);

    void UpdateUI() {
        stepText.text = $"Today's Steps: {stepsToday}";
        xpText.text = $"XP: {currentXP} / {xpToNextLevel}";
        levelText.text = $"Lv. {level}";
    }

    public void AddDebugSteps_Button() {
        StepTrackerBridge.AddDemoSteps(500); // syncs properly — no more overwrite
    }
}
