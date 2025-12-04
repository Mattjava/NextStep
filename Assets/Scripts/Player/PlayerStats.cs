using UnityEngine;
using System;
using Firebase.Database; 


public class PlayerStats : MonoBehaviour {
    public int level = 1;
    public int currentXP = 0;

    private const string XP_KEY = "PLAYER_XP";
    private const string LEVEL_KEY = "PLAYER_LEVEL";

    private Player player; // Don't initialize here
    private DatabaseManager databaseManager;

    public event Action OnXPChanged;
    public event Action OnLevelUp;

    private void Awake() {
        // Get reference to DatabaseManager
        databaseManager = FindObjectOfType<DatabaseManager>();
        
        if (databaseManager == null) {
            Debug.LogError("DatabaseManager not found in scene!");
            return;
        }

        LoadStats();
        LoadPlayerFromDatabase();
    }

    private void LoadPlayerFromDatabase() {
        string id = SystemInfo.deviceUniqueIdentifier;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        reference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted && task.Result.Exists) {
                player = JsonUtility.FromJson<Player>(task.Result.GetRawJsonValue());
                
                // Sync local stats with Firebase if needed
                if (player.experience != currentXP) {
                    currentXP = player.experience;
                    OnXPChanged?.Invoke();
                }
            }
        });
    }

    public int XPToNextLevel => 100 * (level * level) + 50 * level;

    public void AddXP(int amount) {
        if (amount <= 0) return;

        currentXP += amount;
        SaveStats();

        while (currentXP >= XPToNextLevel) {
            currentXP -= XPToNextLevel;
            LevelUp();
        }

        // Update Firebase
        if (databaseManager != null) {
            databaseManager.updateExperience(currentXP);
        }

        OnXPChanged?.Invoke();
    }

    private void LevelUp() {
        level++;
        SaveStats();
        OnLevelUp?.Invoke();
        Debug.Log($"LEVEL UP! You're now level {level}");
    }

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