using UnityEngine;

public static class StepTrackerBridge {
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaObject plugin;
#endif

#if UNITY_EDITOR
    private static int simulatedSteps = 0;
#endif

    public static void Init() {
#if UNITY_ANDROID && !UNITY_EDITOR
        // normal mobile init
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            plugin = new AndroidJavaObject("com.yourcompany.StepPlugin", activity);
        }
#elif UNITY_EDITOR
        simulatedSteps = 0;
        Debug.Log("[DEMO] StepTracker initialized.");
#endif
    }

    public static int GetTodaySteps() {
#if UNITY_ANDROID && !UNITY_EDITOR
                return plugin.Call<int>("getStepsToday");
#elif UNITY_EDITOR
        return simulatedSteps;
#else
                return 0;
#endif
    }

    public static void AddDemoSteps(int amount) {
        Debug.Log("[STEPBRIDGE] AddDemoSteps CALLED");

#if UNITY_EDITOR
        simulatedSteps += amount;
        Debug.Log("[STEPBRIDGE] SimulatedSteps = " + simulatedSteps);

        if (FitBridgeWrapper.Instance != null) {
            FitBridgeWrapper.Instance.stepCount = simulatedSteps;
            FitBridgeWrapper.Instance.UpdateStepText(simulatedSteps);
        }
#endif
    }


}
