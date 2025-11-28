using UnityEngine;
using TMPro;
using System.Collections;

public class QuestCompletePopup : MonoBehaviour {
    public TMP_Text text;
    public float duration = 2f;

    public void Show(string msg) {
        text.text = msg;
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(AutoHide());
    }

    IEnumerator AutoHide() {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
