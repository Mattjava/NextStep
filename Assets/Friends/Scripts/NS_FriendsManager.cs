using System.Collections.Generic;
using UnityEngine;

public class NS_FriendsManager : MonoBehaviour
{
    public static NS_FriendsManager Instance;

    private readonly Dictionary<Player, List<Player>> friends =
        new Dictionary<Player, List<Player>>();

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

    public void AddFriend(Player a, Player b)
    {
        if (a == null || b == null || a == b)
            return;

        if (!friends.ContainsKey(a))
            friends[a] = new List<Player>();
        if (!friends.ContainsKey(b))
            friends[b] = new List<Player>();

        if (!friends[a].Contains(b))
            friends[a].Add(b);
        if (!friends[b].Contains(a))
            friends[b].Add(a);
    }

    public List<Player> GetFriends(Player p)
    {
        if (p == null)
            return new List<Player>();

        if (!friends.ContainsKey(p))
            return new List<Player>();

        return friends[p];
    }
}
