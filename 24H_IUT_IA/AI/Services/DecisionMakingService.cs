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
    private bool ourTurn = false;

    
    protected DecisionMakingService(AI ai)
    {
        Ai = ai;
    }
    
    /// <summary>
    /// Determine une action à prendre.
    /// </summary>
    /// <param name="lastReceivedMessage"></param>
    /// <returns></returns>
    public virtual string? TakeNewAction(string lastReceivedMessage)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// fonction commune que l'on doit lancer à tous les debuts de tour
    /// </summary>
    public string startTurn(string lastReceivedMessage)
    {
        if (this.Ai.MemoryService.TeamName is null)
        {
            return AI.TeamName;
        }
        else
            // si c'est notre tour
        {
            if (lastReceivedMessage.Split('|')[0] == Messages.StartTurn && !ourTurn)
                ourTurn = true;

            if (!ourTurn) return null;

            // si on a pas encore les infos sur les joueurs ou les routes
            if (this.Ai.MemoryService.Players.Count == 0)
                return Messages.PlayersInfo;

            if (this.Ai.MemoryService.Roads.Count == 0)
                return Messages.RoutesInfo;

            // maintena nt qu'on a les infos on va commencer la vraie drunken ia
        }

        throw new InvalidOperationException();
    }


    /// <summary>
    /// Betrays a player.
    /// </summary>
    /// <param name="numJoueur">The number of the player to betray. Must be between 1 and 4.</param>
    /// <returns>A string message indicating the betrayal action and the player number.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the player number is not between 1 and 4.</exception>
    public string Betray(int numJoueur)
    {
        if (numJoueur < 1 || numJoueur > 4)
            throw new ArgumentOutOfRangeException();
        return Messages.Betray + "|" + numJoueur;
    }

    /// <summary>
    /// Pillages a road.
    /// </summary>
    /// <param name="numRoad">The number of the road to pillage. Must be between 1 and 4.</param>
    /// <returns>A string message indicating the pillage action and the road number.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the road number is not between 1 and 4.</exception>
    public string Pillage(int numRoad)
    {
        if (numRoad < 1 || numRoad > 4)
            throw new ArgumentOutOfRangeException();
        return Messages.Pillage + "|" + numRoad;
    }
}