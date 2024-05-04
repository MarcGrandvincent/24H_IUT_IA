﻿namespace _24H_IUT_IA.Models;

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
    public string Activity { get; private set; }

    /// <summary>
    /// Gets the number of chests the player is currently carrying.
    /// </summary>
    public int NumberOfChests { get; private set; }

    /// <summary>
    /// Gets the total value of the loot the player is currently carrying.
    /// </summary>
    public int LootValue { get; private set; }

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
        Activity = playerData[3];
        NumberOfChests = int.Parse(playerData[4]);
        LootValue = int.Parse(playerData[5]);
    }
}