using UnityEngine;
using UnityEngine.UI;

public class AddStepsButton : MonoBehaviour {
    public StepXPManager stepManager;   // or your actual step system script
    public int stepAmount = 500;

    public void AddSteps() {
        stepManager.AddSteps(stepAmount);
    }
}
