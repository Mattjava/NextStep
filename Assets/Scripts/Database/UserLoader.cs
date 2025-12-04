using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

public class UserLoader : MonoBehaviour
{
    private FirebaseAuth auth;
    private DatabaseManager manager;
    // Start is called before the first frame update
    void Start()
    {
       auth = FirebaseAuth.DefaultInstance;
       manager = GetComponent<DatabaseManager>();

       if(auth.CurrentUser != null)
        {
            StartCorountine(LoadData);
        }
    }

    public void LoadData()
    {
        string userId = auth.CurrentUser.UserId;

        Player player = manager.GetPlayer(userId);

        if(player.Exception != null)
        {
            yield break;
        }

        DataSnapshot snapshot = player.Result;

        Player loadedPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
        Debug.Log("Loaded player: " + loadedPlayer.username);
    }
}
