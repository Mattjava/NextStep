using UnityEngine;
using System.Collections.Generic;

public class NS_GuildManager : MonoBehaviour
{
    public static NS_GuildManager Instance;
    private readonly Dictionary<string, NS_Guild> guilds = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public NS_Guild CreateGuild(string name, Player creator)
    {
        NS_Guild g = new NS_Guild(name);
        guilds.Add(g.GuildId, g);
        g.Members.Add(new NS_GuildMember(creator, NS_GuildRank.Leader));
        return g;
    }

    public void Promote(NS_Guild guild, Player player)
    {
        if (guild == null || player == null) return;
        var m = guild.Members.Find(x => x.Player == player);
        if (m != null && m.Rank == NS_GuildRank.Member)
            m.Rank = NS_GuildRank.Officer;
    }

    public void AddGuildXP(Player p, int amount)
    {
        if (p == null) return;
        foreach (var g in guilds.Values)
        {
            foreach (var member in g.Members)
            {
                if (member.Player == p)
                {
                    g.AddGuildXP(amount);
                    return;
                }
            }
        }
    }

    public List<NS_Guild> GetGuildRankings()
    {
        var list = new List<NS_Guild>(guilds.Values);
        list.Sort((a, b) => b.GuildXP.CompareTo(a.GuildXP));
        return list;
    }
}
