using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA: DecisionMakingService
{
    
    public DrunkenIA(AI ai) : base(ai)
    {
        
    }
    
    /// <summary>
    /// Determine une action à prendre.
    /// </summary>
    /// <param name="lastReceivedMessage"> last message received </param>
    /// <returns></returns>
    public override string? TakeNewAction(string lastReceivedMessage)
    {
        startTurn(lastReceivedMessage);
        
        //maintenant que l'on a la méthode startTurne qui est commune a tous les tours on va pouvoir commencer la drunken ia
        
        
        return null;
    }
}