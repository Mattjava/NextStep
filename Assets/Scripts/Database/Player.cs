using SQLite;

public class Player {
    public string id;
    public string username;
    public string password;
    public int experience;
    public int level;

    public Player(string id)
    {
        this.id = id;
        username = "Guest";
        password = "";
        experience = 0;
        level = 1;
    }

    public Player(string username, string password)
    {
        this.id = System.Guid.NewGuid().ToString();
        this.username = username;
        this.password = password;
        experience = 0;
        level = 1;
    }

    public override string ToString()
    {
        return experience + " Level: " + level;
    }

    public void AddExperience(int exp)
    {
        experience += exp;
        while(experience >= level * 100)
        {
            experience -= level * 100;
            level++;
        }
    }

    public int GetExperience()
    {
        return experience;
    }

}