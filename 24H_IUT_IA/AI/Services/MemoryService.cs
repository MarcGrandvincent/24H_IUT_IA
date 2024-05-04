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
    public List<Player> Players { get; set; } = new();

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

        foreach (var player in players) Players.Add(new Player(player));
    }

    /// <summary>
    ///     Parses the route information from the received message.
    /// </summary>
    public void ParseRouteInfo(string messageReceived)
    {
        RoadInitialized = true;
        var roads = messageReceived.Split("|");

        Road? lastRoad = null;

        foreach (var road in roads)
            if (!(road is "True" or "False"))
            {
                lastRoad = new Road(road);
                Roads.Add(lastRoad);
            }
            else if (lastRoad != null)
            {
                lastRoad.IsMonsterPresent = road == "True";
            }
    }

    /// <summary>
    ///     Parses the team number from the received message.
    /// </summary>
    public void ParseTeamNumber(string messageReceived)
    {
        TeamNumber = int.Parse(messageReceived.Split("|")[1]);
    }
}