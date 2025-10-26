﻿using UnityEngine;
using TMPro;
using System.Collections;

public class FitBridgeWrapper : MonoBehaviour {
    public static FitBridgeWrapper Instance { get; private set; }

    private AndroidJavaObject fitBridge;
    public TMP_Text stepText;
    public int stepCount;
    public float refreshInterval = 10f; // seconds between refreshes

    void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            fitBridge = new AndroidJavaObject("com.DefaultCompany.NextStep.FitBridge", activity);
        }

        fitBridge.Call("requestPermissions");
        StartCoroutine(AutoRefreshSteps());
    }

    IEnumerator AutoRefreshSteps() {
        while (true) {
            ReadTodaySteps();
            yield return new WaitForSeconds(refreshInterval);
        }
    }

    public void ReadTodaySteps() {
        fitBridge.Call("readTodaySteps");
    }

    public void OnStepsReceived(string steps) {
        if (int.TryParse(steps, out int parsedSteps)) {
            stepCount = parsedSteps;
            UpdateStepText(stepCount);
        }
    }

    public void UpdateStepText(int steps) {
        if (stepText != null)
            stepText.text = "Steps: " + steps.ToString("N0");
    }

    // Optional: helper for scripts to subscribe to changes
    public delegate void StepUpdateHandler(int steps);
    public event StepUpdateHandler OnStepUpdate;

    private void NotifyStepChange() {
        OnStepUpdate?.Invoke(stepCount);
    }
}
