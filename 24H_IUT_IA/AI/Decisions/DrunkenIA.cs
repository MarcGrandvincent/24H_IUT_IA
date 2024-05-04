using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA: DecisionMakingService
{
    private const string TeamName = "Les alcooliques anonymes"; 
    
    public DrunkenIA(AI ai) : base(ai)
    {
        
    }

    public override string TakeNewAction(string lastReceivedMessage)
    {
        if (this.Ai.MemoryService.TeamName == null)
        {
            this.Ai.MemoryService.TeamName = TeamName;
            return TeamName;
        }

        return Messages.EndGame;
    }
}