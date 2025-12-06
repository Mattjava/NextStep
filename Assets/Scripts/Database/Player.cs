using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player {
    public string id;
    public string username;
    public string password;
    public List<Quest> completedQuests;
    public int experience;
    public int level;

    public Player(string id)
    {
        this.id = id;
        username = "Guest";
        password = "";
        experience = 0;
        level = 1;
        completedQuests = new List<Quest>();
    }

    public Player(string username, string password)
    {
        this.id = System.Guid.NewGuid().ToString();
        this.username = username;
        this.password = password;
        experience = 0;
        level = 1;
        completedQuests = new List<Quest>();
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

    public void AddCompletedQuest(Quest quest)
    {
        completedQuests.Add(quest);
    }
    
    public List<Quest> GetCompletedQuest()
    {
        return completedQuests;
    }
}