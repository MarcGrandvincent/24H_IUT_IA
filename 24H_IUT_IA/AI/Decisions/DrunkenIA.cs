using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA(AI ai) : DecisionMakingService(ai)
{
    public override string? TakeNewAction(string lastReceivedMessage)
    {
        if (this.Ai.MemoryService.TeamName is null)
            return AI.TeamName;
        
        WorkForAction(lastReceivedMessage);

        return null;
    }


    private string WorkForAction(string lastReceivedMessage)
    {
        if (lastReceivedMessage.Split('|')[0] == Messages.StartTurn)
        {
            // Récupère les informations des joueurs.
            if (this.Ai.MemoryService.Players.Count == 0)
                return Messages.PlayersInfo;
                    
            // Récupère les informations des routes.
            if (this.Ai.MemoryService.Roads.Count == 0) 
                return Messages.RoutesInfo;
        }
        
        return "ERROR";
    }
}