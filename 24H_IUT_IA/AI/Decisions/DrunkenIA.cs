using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA(AI ai) : DecisionMakingService(ai)
{
    /// <summary>
    ///     toute la logique de drunken ai
    /// </summary>
    /// <param name="lastReceivedMessage">dernier message reçu</param>
    /// <returns></returns>
    public override string? WorkForAction(string lastReceivedMessage)
    {
        while (lastReceivedMessage != "OK")
        {
            var random = new Random();
            var index = random.Next(Messages.ActionsDrunkenIa.Length);
            return Messages.ActionsDrunkenIa[index];
        }
        
        OurTurn = false;
        return null;
    }
}