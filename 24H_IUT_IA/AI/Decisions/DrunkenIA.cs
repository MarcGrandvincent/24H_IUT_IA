using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA: DecisionMakingService
{
    public DrunkenIA(AI ai) : base(ai)
    {
        
    }

    public override string TakeNewAction(string lastReceivedMessage)
    {
        if (this.Ai.MemoryService.TeamName == null)
            return AI.TeamName;

        return Messages.EndGame;
    }
}