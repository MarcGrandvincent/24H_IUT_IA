using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA: DecisionMakingService
{
    public DrunkenIA(AI ai) : base(ai)
    {
        
    }

    public override string? TakeNewAction(string lastReceivedMessage)
    {
        if (this.Ai.MemoryService.TeamName is null)
        {
            return AI.TeamName;
        }
        else
        {
            if (lastReceivedMessage.Split('|')[0] == Messages.StartTurn)
            {
                if (this.Ai.MemoryService.Players.Count == 0)
                    return Messages.PlayersInfo;
                    
                if (this.Ai.MemoryService.Roads.Count == 0) 
                    return Messages.RoutesInfo;
            }
        }
        
        return null;
    }
}