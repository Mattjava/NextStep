using System;
using System.Collections.Generic;

[Serializable]
public class NS_Guild
{
    public string GuildId;
    public string GuildName;
    public int GuildXP;
    public int GuildLevel;
    public string Description;
    public string Announcement;
    public string EmblemName;
    public List<NS_GuildMember> Members;

    public NS_Guild(string name)
    {
        GuildId = Guid.NewGuid().ToString();
        GuildName = name;
        GuildXP = 0;
        GuildLevel = 1;
        Description = "Welcome to the guild!";
        Announcement = "No announcements yet.";
        EmblemName = "default";
        Members = new List<NS_GuildMember>();
    }

    public void AddGuildXP(int amount)
    {
        GuildXP += amount;
        if (GuildXP >= GuildLevel * 1000)
        {
            GuildXP = 0;
            GuildLevel++;
        }
    }
}
