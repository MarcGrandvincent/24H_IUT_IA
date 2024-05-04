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

    public bool CanBeBetray(Player player)
    {
        if ((this.AttackValue <= player.AttackValue) && this.NumJoueur < player.NumJoueur)
            if (this.Activity == ActivityEnum.PILLER1 || this.Activity == ActivityEnum.PILLER2 || this.Activity == ActivityEnum.PILLER3 || this.Activity == ActivityEnum.PILLER4)
                return true;
    }
    
    /// <summary>
    /// Gets the total value of the loot the player is currently carrying.
    /// </summary>
    public int LootValue { get; private set; }
    
    /// <summary>
    /// Les coffres d'un joueur 
    /// </summary>
    public List<int> Chests { get; set; } = new List<int>();
    
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

    public void MapPlayerData(string data)
    {
        var playerData = data.Split(';');
        Score = int.Parse(playerData[0]);
        AttackValue = int.Parse(playerData[1]); 
        Life = int.Parse(playerData[2]);
        Activity = GetActivity(playerData[3]);
        var newNumberOfChests = int.Parse(playerData[4]);
        var newLootValue = int.Parse(playerData[5]);

        if (newNumberOfChests == 0)
        {
            NumberOfChests = newNumberOfChests;
            Chests.Clear();
        }
        
        if (newNumberOfChests != NumberOfChests && newNumberOfChests != 0)
        {
            if (Chests.Count == 0)
                Chests.Add(newLootValue);
            else 
                Chests.Add(newLootValue - LootValue);
        }

        NumberOfChests = newNumberOfChests;
        LootValue = newLootValue;
    }
}
