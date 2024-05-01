using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DefaultDecisionMaking : DecisionMakingService
{
    public DefaultDecisionMaking(AI ai) : base(ai)
    {
    }

    public override string TakeNewAction(string lastReceivedMessage)
    {
        return Messages.End;
    }
}