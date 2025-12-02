using SQLite

public class Player {
    [PrimaryKey, AutoIncrement]
    public int playerId( get; set; )

    public string username ( get; set; )

    public string password ( get; set; )

    public int xp (get; set; ) = 0;
}