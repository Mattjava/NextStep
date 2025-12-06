using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class NS_GuildProfileUI : MonoBehaviour
{
    [Header("Text UI")]
    public Text guildNameText;
    public Text guildLevelText;
    public Text guildXPText;
    public Text descriptionText;
    public Text announcementText;
    public Text membersText;

    [Header("Emblem")]
    public Image emblemImage;

    [Header("Player")]
    public Player currentPlayer;

    private NS_Guild currentGuild;

    private void OnEnable()
    {
        FindGuildForCurrentPlayer();
        Refresh();
    }

    private void FindGuildForCurrentPlayer()
    {
        currentGuild = null;

        if (currentPlayer == null || NS_GuildManager.Instance == null)
            return;

        List<NS_Guild> allGuilds = NS_GuildManager.Instance.GetGuildRankings();

        foreach (NS_Guild g in allGuilds)
        {
            foreach (NS_GuildMember m in g.Members)
            {
                if (m.Player == currentPlayer)
                {
                    currentGuild = g;
                    return;
                }
            }
        }
    }

    public void Refresh()
    {
        if (currentGuild == null)
        {
            guildNameText.text = "No Guild";
            guildLevelText.text = "";
            guildXPText.text = "";
            descriptionText.text = "You are not in a guild.";
            announcementText.text = "";
            membersText.text = "";
            emblemImage.sprite = null;
            return;
        }

        guildNameText.text = currentGuild.GuildName;
        guildLevelText.text = "Level " + currentGuild.GuildLevel;
        guildXPText.text = "XP: " + currentGuild.GuildXP;
        descriptionText.text = currentGuild.Description;
        announcementText.text = currentGuild.Announcement;

        // 🔥 FIX — replace Player.name → Player.username
        StringBuilder sb = new StringBuilder();
        foreach (NS_GuildMember m in currentGuild.Members)
        {
            string name = m.Player != null ? m.Player.username : "(no player)";
            sb.AppendLine(name + " (" + m.Rank + ")");
        }
        membersText.text = sb.ToString();

        if (emblemImage != null && NS_GuildEmblemManager.Instance != null)
            emblemImage.sprite = NS_GuildEmblemManager.Instance.GetEmblem(currentGuild.EmblemName);
    }
}
