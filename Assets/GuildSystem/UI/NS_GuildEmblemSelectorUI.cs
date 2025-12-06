using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NS_GuildEmblemSelectorUI : MonoBehaviour
{
    public Image previewImage;
    public Player currentPlayer;

    private NS_Guild currentGuild;
    private int currentIndex = 0;

    private void OnEnable()
    {
        FindGuildForCurrentPlayer();
        UpdatePreviewFromGuild();
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

    public void NextEmblem()
    {
        if (NS_GuildEmblemManager.Instance == null ||
            NS_GuildEmblemManager.Instance.Emblems == null ||
            NS_GuildEmblemManager.Instance.Emblems.Length == 0 ||
            currentGuild == null)
            return;

        currentIndex++;
        if (currentIndex >= NS_GuildEmblemManager.Instance.Emblems.Length)
            currentIndex = 0;

        ApplyCurrentEmblem();
    }

    public void PreviousEmblem()
    {
        if (NS_GuildEmblemManager.Instance == null ||
            NS_GuildEmblemManager.Instance.Emblems == null ||
            NS_GuildEmblemManager.Instance.Emblems.Length == 0 ||
            currentGuild == null)
            return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = NS_GuildEmblemManager.Instance.Emblems.Length - 1;

        ApplyCurrentEmblem();
    }

    private void ApplyCurrentEmblem()
    {
        if (currentGuild == null ||
            NS_GuildEmblemManager.Instance == null ||
            NS_GuildEmblemManager.Instance.Emblems == null ||
            NS_GuildEmblemManager.Instance.Emblems.Length == 0)
            return;

        Sprite sprite = NS_GuildEmblemManager.Instance.Emblems[currentIndex];
        currentGuild.EmblemName = sprite != null ? sprite.name : string.Empty;

        if (previewImage != null)
            previewImage.sprite = sprite;
    }

    private void UpdatePreviewFromGuild()
    {
        if (currentGuild == null ||
            NS_GuildEmblemManager.Instance == null ||
            NS_GuildEmblemManager.Instance.Emblems == null ||
            NS_GuildEmblemManager.Instance.Emblems.Length == 0)
            return;

        if (string.IsNullOrEmpty(currentGuild.EmblemName))
        {
            currentIndex = 0;
            ApplyCurrentEmblem();
            return;
        }

        Sprite[] arr = NS_GuildEmblemManager.Instance.Emblems;
        for (int i = 0; i < arr.Length; i++)
        {
            Sprite s = arr[i];
            if (s != null && s.name == currentGuild.EmblemName)
            {
                currentIndex = i;
                if (previewImage != null)
                    previewImage.sprite = s;
                return;
            }
        }

        currentIndex = 0;
        ApplyCurrentEmblem();
    }
}
