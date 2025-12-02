using UnityEngine;
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
}