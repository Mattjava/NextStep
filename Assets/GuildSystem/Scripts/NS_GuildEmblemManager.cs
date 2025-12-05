using UnityEngine;

public class NS_GuildEmblemManager : MonoBehaviour
{
    public static NS_GuildEmblemManager Instance;
    public Sprite[] Emblems;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Sprite GetEmblem(string name)
    {
        if (Emblems == null || Emblems.Length == 0 || string.IsNullOrEmpty(name)) return null;
        foreach (var e in Emblems)
            if (e != null && e.name == name) return e;
        return null;
    }
}
