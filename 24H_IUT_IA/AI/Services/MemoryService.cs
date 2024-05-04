using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Services;

/// <summary>
/// la ou on va stocker tous nos objets de données 
/// </summary>
public class MemoryService
{
    public bool PlayersInitialized { get; set; }
    public bool RoadInitialized { get; set; }
    public string? TeamName { get; set; } = null;

    private int _teamNumber = 0;
    public List<Player> Players { get; set; } = [new(), new(), new(), new()];
    public List<Road> Roads { get; set; } = new List<Road>();

    public int Turn { get; set; } = 0;

    public Player GetOurPlayerInfo()
    {
        return Players[_teamNumber - 1];
    }

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

    public void ParseRouteInfo(string messageReceived)
    {
        RoadInitialized = true;
        var roads = messageReceived.Split("|");

        Road? lastRoad = null;

        foreach (var road in roads)
        {
            if (!(road is "True" or "False"))
            {
                lastRoad = new Road(road);
                Roads.Add(lastRoad);
            }
            else if (lastRoad != null) lastRoad.IsMonsterPresent = road == "True";
        }
    }

    public void ParseTeamNumber(string messageReceived)
    {
        _teamNumber = int.Parse(messageReceived.Split("|")[1]);
    }
}