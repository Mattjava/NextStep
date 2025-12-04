using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class NS_FriendsListUI : MonoBehaviour
{
    public Player currentPlayer;
    public Text friendsText;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (friendsText == null)
            return;

        if (NS_FriendsManager.Instance == null || currentPlayer == null)
        {
            friendsText.text = "No player or manager assigned.";
            return;
        }

        List<Player> list = NS_FriendsManager.Instance.GetFriends(currentPlayer);

        if (list.Count == 0)
        {
            friendsText.text = "You have no friends added.";
            return;
        }

        StringBuilder sb = new StringBuilder();

        foreach (Player f in list)
        {
            // 🔥 FIX — Player.name → Player.username
            string name = f != null ? f.username : "(null)";
            sb.AppendLine(name);
        }

        friendsText.text = sb.ToString();
    }
}
