using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System.Collections;

public class DatabaseManager : MonoBehaviour
{
    public InputField Username;
    public InputField Password;

    private string id;
    private DatabaseReference reference;

    void Start()
    {
        id = SystemInfo.deviceUniqueIdentifier;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //public void RegisterUser()
    //{
    //    string username = Username.text;
    //    string password = Password.text;
    //    Player newPlayer = new Player(username, password);
    //    string json = JsonUtility.ToJson(newPlayer);
    //    reference.Child("players").Child(id).SetRawJsonValueAsync(json).ContinueWith(task => {
    //        if (task.IsCompleted)
    //        {
    //            Debug.Log("User registered successfully.");
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to register user: " + task.Exception);
    //        }
    //    });
    //}

    //public void LoginUser()
    //{
    //    reference.Child("players").Child(id).GetValueAsync().ContinueWith(task => {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            if (snapshot.Exists)
    //            {
    //                Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
    //                if (existingPlayer.password == Password.text && existingPlayer.username == Username.text)
    //                {
    //                    Debug.Log("Login successful: " + existingPlayer.ToString());
    //                }
    //                else
    //                {
    //                    Debug.LogError("Invalid username or password.");
    //                }
    //            }
    //            else
    //            {
    //                Debug.LogError("User does not exist.");
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to retrieve user: " + task.Exception);
    //        }
    //    });
    //}

    public IEnumerator GetPlayer(System.Action<Player> callback)
    {
        var userData = reference.Child("users").Child(id).GetValueAsync();

        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        if(userData != null)
        {
            DataSnapshot snapshot = userData.Result;
            Player player = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
            callback.Invoke(player);
        }
    }

    public IEnumerator getExperience(System.Action<int> callback)
    {
        var userExp = reference.Child("players").Child(id).Child("experience").GetValueAsync();

        yield return new WaitUntil(predicate: () => userExp.IsCompleted);

        if (userExp != null)
        {
            DataSnapshot snapshot = userExp.Result;

            callback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    public IEnumerator getLevel(System.Action<int> callback)
    {
        var userLevel = reference.Child("players").Child(id).Child("level").GetValueAsync();

        yield return new WaitUntil(predicate: () => userLevel.IsCompleted);

        if(userLevel != null)
        {
            DataSnapshot snapshot = userLevel.Result;

            callback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }


    public void updateExperience(int newExperience)
    {
        reference.Child("players").Child(id).Child("level").SetValueAsync(newExperience);
    }

    public void updateLevel(int newLevel)
    {
        reference.Child("player").Child(id).Child("level").SetValueAsync(newLevel);
    }
}