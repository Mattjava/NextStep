using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class NS_GuildChatUI : MonoBehaviour
{
    public Text chatContentText;
    public InputField messageInput;

    public Player currentPlayer;

    private NS_Guild currentGuild;

    private void OnEnable()
    {
        FindGuildForCurrentPlayer();
        RefreshChat();
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

    public void OnSendMessageButton()
    {
        if (currentGuild == null || currentPlayer == null)
            return;

        if (messageInput == null)
            return;

        string msg = messageInput.text.Trim();
        if (string.IsNullOrEmpty(msg))
            return;

        if (NS_GuildChat.Instance != null)
        {
            NS_GuildChat.Instance.SendMessage(currentGuild, currentPlayer, msg);
        }

        messageInput.text = string.Empty;
        RefreshChat();
    }

    public void RefreshChat()
    {
        if (chatContentText == null)
            return;

        if (currentGuild == null || NS_GuildChat.Instance == null)
        {
            chatContentText.text = "You are not in a guild.";
            return;
        }

        List<string> messages = NS_GuildChat.Instance.GetMessages(currentGuild);
        StringBuilder sb = new StringBuilder();
        foreach (string line in messages)
        {
            sb.AppendLine(line);
        }
        chatContentText.text = sb.ToString();
    }
}
