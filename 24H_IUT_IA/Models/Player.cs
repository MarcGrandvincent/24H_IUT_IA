namespace _24H_IUT_IA.Models;

/// <summary>
/// Represents a player in the game.
/// </summary>
public class Player
{
    /// <summary>
    /// Gets the score of the player.
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// Gets the attack value of the player.
    /// </summary>
    public int AttackValue { get; private set; }

    /// <summary>
    /// Gets the life points of the player.
    /// </summary>
    public int Life { get; private set; }

    /// <summary>
    /// Gets the current activity of the player.
    /// </summary>
    public ActivityEnum Activity { get; private set; }

    /// <summary>
    /// Gets the number of chests the player is currently carrying.
    /// </summary>
    public int NumberOfChests { get; private set; }

    /// <summary>
    /// Gets the total value of the loot the player is currently carrying.
    /// </summary>
    public int LootValue { get; private set; }
    
    public List<int> Chests { get; set; } = new List<int>();

    /// <summary>
    /// Initializes a new instance of the Player class.
    /// </summary>
    /// <param name="data">A semicolon-separated string containing the player's data.</param>
    public Player(string data)
    {
        var playerData = data.Split(';');
        Score = int.Parse(playerData[0]);
        AttackValue = int.Parse(playerData[1]);
        Life = int.Parse(playerData[2]);
        Activity = GetActivity(playerData[3]);
        NumberOfChests = int.Parse(playerData[4]);
        LootValue = int.Parse(playerData[5]);
    }
    
    private ActivityEnum GetActivity(string activity)
    {
        return activity switch
        {
            "PILLER1" => ActivityEnum.PILLER1,
            "PILLER2" => ActivityEnum.PILLER2,
            "PILLER3" => ActivityEnum.PILLER3,
            "PILLER4" => ActivityEnum.PILLER4,
            "REPARATION" => ActivityEnum.REPARATION,
            "RECELE" => ActivityEnum.RECELE,
            "ATTAQUE" => ActivityEnum.ATTAQUE,
            "AUCUNE" => ActivityEnum.AUCUNE
        };
    }
}
