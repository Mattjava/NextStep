using UnityEngine;
using SQLite;
using System.IO;


public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection db;

    void Awake()
    {
        String path = Path.Combine(Application.persistantDataPath, "game.db");
        db = new SQLiteConnection(path);


        db.CreateTable<Player>();
    }
}