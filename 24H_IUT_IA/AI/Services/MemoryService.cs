using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Services;

/// <summary>
/// la ou on va stocker tous nos objets de données 
/// </summary>
public class MemoryService
{
    public bool PlayersInitialized { get; set; }
    public bool RoadInitialized { get; set; }

    /// <summary>
    ///     The name of the team.
    /// </summary>
    public string? TeamName { get; set; } = null;

    /// <summary>
    ///     The number of the team.
    /// </summary>
    public int TeamNumber { get; set; }

    /// <summary>
    ///     A list of players.
    /// </summary>
    public List<Player> Players { get; set; } = [new Player(), new Player(), new Player(), new Player()];

/// <summary>
    ///     A list of roads.
    /// </summary>
    public List<Road> Roads { get; set; } = new();

    /// <summary>
    ///     The current turn.
    /// </summary>
    public int Turn { get; set; } = 0;

    /// <summary>
    ///     Returns the information of our player.
    /// </summary>
    public Player GetOurPlayerInfo()
    {
        return Players[TeamNumber - 1];
    }

    /// <summary>
    ///     Parses the players's information from the received message.
    /// </summary>
    public void ParserPlayersInfo(string messageReceived)
    {
        PlayersInitialized = true;
        var players = messageReceived.Split("|");

        for (var index = 0; index < players.Length; index++)
        {
            var player = players[index];
            Players[index].MapPlayerData(player);
        }
        
    }

    /// <summary>
    ///     Parses the route information from the received message.
    /// </summary>
    public void ParseRouteInfo(string messageReceived)
    {
        RoadInitialized = true;
        Roads.Clear();
        var roads = messageReceived.Split("|");

        Road? lastRoad = null;

        foreach (var road in roads)
        { 
            new Road(road);
        }
    }

    /// <summary>
    ///     Parses the team number from the received message.
    /// </summary>
    public void ParseTeamNumber(string messageReceived)
    {
        TeamNumber = int.Parse(messageReceived.Split("|")[1]);
    }

    /// <summary>
    /// met à jour l'ordre de passage des joueurs
    /// </summary>
    public void UpdateOrder()
    {
        switch (Turn % 4)
        {
            case 1:
                Players[0].Order = 1;
                Players[1].Order = 2;
                Players[2].Order = 3;
                Players[3].Order = 4;
                break;
            case 2:
                Players[0].Order = 2;
                Players[1].Order = 3;
                Players[2].Order = 4;
                Players[3].Order = 1;
                break;
            case 3:
                Players[0].Order = 3;
                Players[1].Order = 4;
                Players[2].Order = 1;
                Players[3].Order = 2;
                break;
            case 0:
                Players[0].Order = 4;
                Players[1].Order = 1;
                Players[2].Order = 2;
                Players[3].Order = 3;
                break;
        }
    }
}