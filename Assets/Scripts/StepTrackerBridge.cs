public static class StepTrackerBridge {
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaObject plugin;
#endif

    public static void Init() {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            plugin = new AndroidJavaObject("com.yourcompany.StepPlugin", activity);
        }
#elif UNITY_IOS && !UNITY_EDITOR
        InitHealthKit();
#endif
    }

    public static int GetTodaySteps() {
#if UNITY_ANDROID && !UNITY_EDITOR
        return plugin.Call<int>("getStepsToday");
#elif UNITY_IOS && !UNITY_EDITOR
        return GetHealthKitSteps();
#else
        return 0;
#endif
    }

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void InitHealthKit();
    [DllImport("__Internal")]
    private static extern int GetHealthKitSteps();
#endif
}
