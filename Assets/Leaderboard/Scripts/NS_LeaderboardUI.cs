using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class NS_LeaderboardUI : MonoBehaviour
{
    public Text leaderboardText;
    public int maxEntries = 10;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (leaderboardText == null)
            return;

        if (NS_LeaderboardManager.Instance == null)
        {
            leaderboardText.text = "LeaderboardManager not present in scene.";
            return;
        }

        List<NS_LeaderboardManager.Entry> top =
            NS_LeaderboardManager.Instance.GetTopEntries(maxEntries);

        if (top.Count == 0)
        {
            leaderboardText.text = "No scores reported yet.";
            return;
        }

        StringBuilder sb = new StringBuilder();
        int rank = 1;

        foreach (NS_LeaderboardManager.Entry e in top)
        {
            // 🔥 FIX — Player.name → Player.username
            string name = e.Player != null ? e.Player.username : "(null)";
            sb.AppendLine(rank + ". " + name + " — " + e.Score);
            rank++;
        }

        leaderboardText.text = sb.ToString();
    }
}
