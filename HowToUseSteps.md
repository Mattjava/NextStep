In C# file use this to get the steps from the tracking integration:
```
int steps = FitBridgeWrapper.Instance.stepCount;
```

Also if you want to handle step changes in real time, add these to your script:
```
void Start() {
    FitBridgeWrapper.Instance.OnStepUpdate += HandleStepChange;
}

void HandleStepChange(int newSteps) {
    Debug.Log("Updated steps: " + newSteps);
    // Trigger loot drop or whatever
}
```
