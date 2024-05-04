using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Services;

public abstract class DecisionMakingService
{
    /// <summary>
    /// L'IA en cours.
    /// </summary>
    public AI Ai { get; set; }

    /// <summary>
    /// Détermine si c'est notre tour.
    /// </summary>
    public bool OurTurn { get; set; } = false;

    
    protected DecisionMakingService(AI ai)
    {
        Ai = ai;
    }
    
    /// <summary>
    /// Determine une action à prendre.
    /// </summary>
    /// <param name="lastReceivedMessage"> dernier message reçu</param>
    /// <returns></returns>
    public virtual string? TakeNewAction(string lastReceivedMessage)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// fonction commune que l'on doit lancer à tous les debuts de tour
    /// </summary>
    public string? StartTurn(string lastReceivedMessage)
    {
        if (this.Ai.MemoryService.TeamName is null)
        {
            return AI.TeamName;
        }
        else
            // si c'est notre tour
        {
            if (lastReceivedMessage.Split('|')[0] == Messages.StartTurn && !OurTurn)
                OurTurn = true;

            if (!OurTurn) return null;

            // si on a pas encore les infos sur les joueurs ou les routes
            if (this.Ai.MemoryService.Players.Count == 0)
                return Messages.PlayersInfo;

            if (this.Ai.MemoryService.Roads.Count == 0)
                return Messages.RoutesInfo;

            // si on a les infos sur les joueurs et les routes
            return null;
        }
    }
}