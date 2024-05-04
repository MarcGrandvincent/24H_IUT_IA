using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class DrunkenIA(AI ai) : DecisionMakingService(ai)
{
    public override string? TakeNewAction(string lastReceivedMessage)
    {
        //appel de la methode commune à tous les débuts de tour
        if (StartTurn(lastReceivedMessage) is not null || !OurTurn)
            return StartTurn(lastReceivedMessage);
        else 
        {
            //debut de drunken ai 
            return WorkForAction(lastReceivedMessage);  
        }
    }


    /// <summary>
    ///     toute la logique de drunken ai
    /// </summary>
    /// <param name="lastReceivedMessage">dernier message reçu</param>
    /// <returns></returns>
    private string WorkForAction(string lastReceivedMessage)
    {
        var random = new Random();
        var index = random.Next(Messages.ActionsDrunkenIa.Length);
        return Messages.ActionsDrunkenIa[index];
    }
}