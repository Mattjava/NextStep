using SQLite;

public class Player {
    public string username;
    public string password;
    public int experience;
    public int level;

    public Player(string username, string password)
    {
        this.password = password;
        this.username = username;
        experience = 0;
        level = 1;
    }

    public override string ToString()
    {
        return "Username: " + username + " Password: " + password + " Experience: " + experience + " Level: " + level;
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