namespace _24H_IUT_IA.Models;

/// <summary>
/// Represents a Road in the game.
/// </summary>
public class Road
{
    /// <summary>
    /// Gets or sets the level of the boat.
    /// </summary>
    public int LevelBoat{ get; set; };

    /// <summary>
    /// Gets or sets the attack value of the merchant ship.
    /// </summary>
    public int AttackValue{ get; set; };

    /// <summary>
    /// Gets or sets the value of the first chest.
    /// </summary>
    public int ChestValue1{ get; set; };

    /// <summary>
    /// Gets or sets the value of the second chest.
    /// </summary>
    public int ChestValue2{ get; set; };

    /// <summary>
    /// Gets or sets the value of the third chest.
    /// </summary>
    public int ChestValue3{ get; set; };

    /// <summary>
    /// Gets or sets a value indicating whether a monster is present on the road.
    /// </summary>
    public bool IsMonsterPresent { get; set; };
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Road"/> class.
    /// </summary>
    /// <param name="reciveMessage">The message received.</param>
    public Road(string reciveMessage)
    {
        string[] road = reciveMessage.Split(";");
        LevelBoat = int.Parse(road[0]);
        AttackValue = int.Parse(road[1]);
        ChestValue1 = int.Parse(road[2]);
        ChestValue2 = int.Parse(road[3]);
        ChestValue3 = int.Parse(road[4]);
        IsMonsterPresent = road[5] == "1";
    }
}