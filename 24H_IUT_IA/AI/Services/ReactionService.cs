using System.Runtime.CompilerServices;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Services;

public class ReactionService
{
    public ReactionService(AI ai)
    {
        Ai = ai;
    }
    
    /// <summary>
    /// L'IA en cours.
    /// </summary>
    public AI Ai { get; set; }
    
    /// <summary>
    /// Réaction au message reçu.
    /// </summary>
    /// <param name="sentMessage">Le dernier message envoyé.</param>
    /// <param name="receivedMessage">Le dernier message reçu.</param>
    public void ReactToMessage(string sentMessage, string receivedMessage)
    {
        switch (sentMessage)
        {
            case Messages.PlayersInfo:
                GetPlayersInfo(receivedMessage);
                break;
            case Messages.RoutesInfo:
                GetRouteInfo(receivedMessage);
                break;
            case AI.TeamName:
                this.Ai.MemoryService.TeamName = AI.TeamName;
                break;
            
        }
    }
    
    public void GetPlayersInfo(string messageReceived)
    {
        Ai.MemoryService.ParserPlayersInfo(messageReceived);
    }
    
    public void GetRouteInfo(string messageReceived)
    {
        Ai.MemoryService.ParserRouteInfo(messageReceived);
    }
    
    public void GetTeamNumber(string messageReceived)
    {
        Ai.MemoryService.ParserTeamNumber(messageReceived);
    }
}