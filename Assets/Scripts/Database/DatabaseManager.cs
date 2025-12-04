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

    public void RegisterUser()
    {
        string username = Username.text;
        string password = Password.text;
        Player newPlayer = new Player(username, password);
        string json = JsonUtility.ToJson(newPlayer);
        reference.Child("users").Child(id).SetRawJsonValueAsync(json).ContinueWith(task => {
            if (task.IsCompleted)
            {
                Debug.Log("User registered successfully.");
            }
            else
            {
                Debug.LogError("Failed to register user: " + task.Exception);
            }
        });
    }

    public void LoginUser()
    {
        reference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
                    if (existingPlayer.password == Password.text && existingPlayer.username == Username.text)
                    {
                        Debug.Log("Login successful: " + existingPlayer.ToString());
                    }
                    else
                    {
                        Debug.LogError("Invalid username or password.");
                    }
                }
                else
                {
                    Debug.LogError("User does not exist.");
                }
            }
            else
            {
                Debug.LogError("Failed to retrieve user: " + task.Exception);
            }
        });
    }

    public void GetPlayer(System.Action<Player> callback)
    {
        reference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
                    callback?.Invoke(existingPlayer);
                }
                else
                {
                    Debug.LogError("User does not exist.");
                    callback?.Invoke(null);
                }
            }
            else
            {
                Debug.LogError("Failed to retrieve user: " + task.Exception);
                callback?.Invoke(null);
            }
        });
    }

    public void getExperience(System.Action<int> callback)
    {
        reference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Player existingPlayer = JsonUtility.FromJson<Player>(snapshot.GetRawJsonValue());
                    callback?.Invoke(existingPlayer.experience);
                }
                else
                {
                    Debug.LogError("User does not exist.");
                    callback?.Invoke(0);
                }
            }
            else
            {
                Debug.LogError("Failed to retrieve user: " + task.Exception);
                callback?.Invoke(0);
            }
        });
    }

    public void updateExperience(int newExperience)
    {

    }
}