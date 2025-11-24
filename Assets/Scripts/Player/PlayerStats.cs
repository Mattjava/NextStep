using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int level = 1;
    public int currentXP = 0;

    // You can tweak this to adjust leveling speed
    public AnimationCurve xpCurve;

    public event Action OnXPChanged;
    public event Action OnLevelUp;

    // How much XP needed to reach the next level
    public int XPToNextLevel {
        get {
            // Example curve: xpCurve.Evaluate(level) * 100
            return Mathf.CeilToInt(xpCurve.Evaluate(level) * 100);
        }
    }

    public void AddXP(int amount) {
        if (amount <= 0) return;

        currentXP += amount;
        Debug.Log($"XP Gained {amount} XP. Total: {currentXP}/{XPToNextLevel}");

        while (currentXP >= XPToNextLevel) {
            currentXP -= XPToNextLevel;
            LevelUp();
        }

        OnXPChanged?.Invoke();
    }

    private void LevelUp() {
        level++;
        Debug.Log($"LEVEL UP! Now lvl {level}.");
        OnLevelUp?.Invoke();
    }
}
