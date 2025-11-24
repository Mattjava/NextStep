using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour {
    public int level = 1;
    public int currentXP = 0;

    public AnimationCurve xpCurve;

    private const string XP_KEY = "PLAYER_XP";
    private const string LEVEL_KEY = "PLAYER_LEVEL";

    public event Action OnXPChanged;
    public event Action OnLevelUp;

    private void Awake() {
        LoadStats();
    }

    public int XPToNextLevel => Mathf.CeilToInt(xpCurve.Evaluate(level) * 100);

    public void AddXP(int amount) {
        if (amount <= 0) return;

        currentXP += amount;
        SaveStats();

        while (currentXP >= XPToNextLevel) {
            currentXP -= XPToNextLevel;
            LevelUp();
        }

        OnXPChanged?.Invoke();
    }

    private void LevelUp() {
        level++;
        SaveStats();
        OnLevelUp?.Invoke();
        Debug.Log($"LEVEL UP! You're now level {level}");
    }

    // ===========================================
    // SAVE + LOAD
    // ===========================================
    public void SaveStats() {
        PlayerPrefs.SetInt(XP_KEY, currentXP);
        PlayerPrefs.SetInt(LEVEL_KEY, level);
        PlayerPrefs.Save();
    }

    private void LoadStats() {
        if (PlayerPrefs.HasKey(XP_KEY))
            currentXP = PlayerPrefs.GetInt(XP_KEY);

        if (PlayerPrefs.HasKey(LEVEL_KEY))
            level = PlayerPrefs.GetInt(LEVEL_KEY);

        Debug.Log($"Loaded XP: {currentXP}, Level: {level}");
    }
}
