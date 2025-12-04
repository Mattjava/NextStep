using System.Collections.Generic;
using UnityEngine;

public class NS_GuildChat : MonoBehaviour
{
    public static NS_GuildChat Instance;

    private readonly Dictionary<string, List<string>> chatHistory =
        new Dictionary<string, List<string>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SendMessage(NS_Guild guild, Player sender, string message)
    {
        if (guild == null || sender == null || string.IsNullOrWhiteSpace(message)) return;

        // FIX: ContainsKey
        if (!chatHistory.ContainsKey(guild.GuildId))
            chatHistory[guild.GuildId] = new List<string>();

        // FIX: sender.username
        string senderName = sender.username;
        chatHistory[guild.GuildId].Add(senderName + ": " + message);
    }

    public List<string> GetMessages(NS_Guild guild)
    {
        if (guild == null) return new List<string>();

        if (!chatHistory.ContainsKey(guild.GuildId)) return new List<string>();

        return chatHistory[guild.GuildId];
    }
}
