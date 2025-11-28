using UnityEngine;
using TMPro;
using System.Collections;
using System.Runtime.InteropServices;

public class FitBridgeWrapper : MonoBehaviour {
    public static FitBridgeWrapper Instance { get; private set; }

    private AndroidJavaObject fitBridge;
    public TMP_Text stepText;
    public int stepCount;
    public float refreshInterval = 10f; // seconds between refreshes

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void _RequestHealthKitPermissions();

    [DllImport("__Internal")]
    private static extern void _GetTodaySteps();
#endif

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            fitBridge = new AndroidJavaObject("com.DefaultCompany.NextStep.FitBridge", activity);
        }

        fitBridge.Call("requestPermissions");
        StartCoroutine(AutoRefreshSteps());

#elif UNITY_IOS && !UNITY_EDITOR
        _RequestHealthKitPermissions();
        StartCoroutine(AutoRefreshSteps());

#else
        // --- EDITOR / PC DEMO MODE ---
        Debug.Log("[DEMO] FitBridgeWrapper running in Editor - using StepTrackerBridge simulated steps.");
        StepTrackerBridge.Init();
        StartCoroutine(EditorRefreshSteps());
#endif
    }

    // ============ MOBILE (Android + iOS) ============
    public void RequestPermissions() {
#if UNITY_ANDROID && !UNITY_EDITOR
        fitBridge.Call("requestPermissions");
#elif UNITY_IOS && !UNITY_EDITOR
        _RequestHealthKitPermissions();
#else
        Debug.Log("RequestPermissions called in Editor");
#endif
    }

    public void ReadTodaySteps() {
#if UNITY_ANDROID && !UNITY_EDITOR
        fitBridge.Call("readTodaySteps");
#elif UNITY_IOS && !UNITY_EDITOR
        _GetTodaySteps();
#else
        Debug.Log("ReadTodaySteps called in Editor");
#endif
    }

    IEnumerator AutoRefreshSteps() {
        while (true) {
            ReadTodaySteps();
            yield return new WaitForSeconds(refreshInterval);
        }
    }

    // Called from native mobile plugin
    public void OnStepsReceived(string steps) {
        if (int.TryParse(steps, out int parsedSteps)) {
            stepCount = parsedSteps;
            UpdateStepText(stepCount);
            NotifyStepChange();
        }
    }

    // ============ EDITOR / PC DEMO MODE ============
#if UNITY_EDITOR
    IEnumerator EditorRefreshSteps() {
        while (true) {
            // DO NOT overwrite simulated steps here
            UpdateStepText(stepCount);
            NotifyStepChange();
            yield return new WaitForSeconds(0.25f);
        }
    }
#endif

    // ============ SHARED UI UPDATE ============
    public void UpdateStepText(int steps) {
        if (stepText != null)
            stepText.text = "Today's Steps: " + steps.ToString("N0");
    }

    public delegate void StepUpdateHandler(int steps);
    public event StepUpdateHandler OnStepUpdate;

    private void NotifyStepChange() {
        OnStepUpdate?.Invoke(stepCount);
    }
}
