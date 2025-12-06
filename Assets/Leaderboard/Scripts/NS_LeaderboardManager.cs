using System.Collections.Generic;
using UnityEngine;

public class NS_LeaderboardManager : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public Player Player;
        public int Score;
    }

    public static NS_LeaderboardManager Instance;

    private readonly List<Entry> entries = new List<Entry>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optional: DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReportScore(Player player, int score)
    {
        if (player == null)
            return;

        Entry existing = entries.Find(e => e.Player == player);
        if (existing != null)
        {
            existing.Score = score;
        }
        else
        {
            entries.Add(new Entry
            {
                Player = player,
                Score = score
            });
        }
    }

    public List<Entry> GetTopEntries(int count)
    {
        List<Entry> copy = new List<Entry>(entries);
        copy.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (count < 0 || count > copy.Count)
            count = copy.Count;
        return copy.GetRange(0, count);
    }
}
