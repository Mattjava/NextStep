using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

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
    //    reference.Child("users").Child(id).SetRawJsonValueAsync(json).ContinueWith(task => {
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
    //    reference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
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

    //public Player GetPlayer()
    //{
    //    reference.Child("player").Child(id).GetValueAsync().ContinueWith(task => {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            if (snapshot.Exists)
    //            {
    //                Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
    //                return existingPlayer;
    //            }
    //            else
    //            {
    //                Debug.LogError("User does not exist.");
    //                return null;
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to retrieve user: " + task.Exception);
    //            return null;
    //        }
    //    });

    //    return null;
    //}

    //public int getExperience()
    //{
    //    reference.Child("players").Child(id).GetValueAsync().ContinueWith(task => {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            if (snapshot.Exists)
    //            {
    //                Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
    //                return existingPlayer.experience;
    //            }
    //            else
    //            {
    //                Debug.LogError("User does not exist.");
    //                return null;
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Failed to retrieve user: " + task.Exception);
    //            return null;
    //        }
    //    });

    //    return null;
    //}

    //public void updateExperience(int exp)
    //{
    //    reference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            if (snapshot.Exists)
    //            {
    //                Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
    //                existingPlayer.AddExperience(exp);
    //                string json = JsonUtility.ToJson(existingPlayer);
    //                reference.Child("users").Child(id).SetRawJsonValueAsync(json).ContinueWith(updateTask => {
    //                    if (updateTask.IsCompleted)
    //                    {
    //                        Debug.Log("Experience updated successfully.");
    //                    }
    //                    else
    //                    {
    //                        Debug.LogError("Failed to update experience: " + updateTask.Exception);
    //                    }
    //                });
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
}