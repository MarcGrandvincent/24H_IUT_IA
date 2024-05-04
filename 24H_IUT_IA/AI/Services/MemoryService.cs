using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Services;

/// <summary>
/// la ou on va stocker tous nos objets de données 
/// </summary>
public class MemoryService
{
    public string? TeamName { get; set; } = null;
    public int TeamNumber { get; set; } = 0;
    public List<Player> Players { get; set; } = [];
    public List<Road> Roads { get; set; } = [];
    
    public int Turn { get; set; } = 0;

    public void ParserPlayersInfo(string messageReceived)
    {
        var players = messageReceived.Split("|");

        foreach (var player in players)
        {
            Players.Add(new Player(player));
        }
    }
    
    public void ParserRouteInfo(string messageReceived)
    {
        var roads = messageReceived.Split("|");

        foreach (var road in roads)
        {
            Roads.Add(new Road(road));
        }
    }
    
    public void ParserTeamNumber(string messageReceived)
    {
        TeamNumber = int.Parse(messageReceived.Split("|")[1]);
    }
}
