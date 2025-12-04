using System;

[Serializable]
public class NS_GuildMember
{
    public Player Player;
    public NS_GuildRank Rank;

    public NS_GuildMember(Player player, NS_GuildRank rank)
    {
        Player = player;
        Rank = rank;
    }
}
