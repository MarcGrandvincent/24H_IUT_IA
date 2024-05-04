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

    public List<string> Orders = new List<string>();
    
    
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
        if (this.Ai.MemoryService.TeamName is null)
        {
            return AI.TeamName;
        }
        else
        {
            if (lastReceivedMessage.Contains(Messages.StartTurn) && !OurTurn)
            {
                Orders.Clear();
                OurTurn = true;
                this.Ai.MemoryService.RoadInitialized = false;
                this.Ai.MemoryService.PlayersInitialized = false;
            }
                
            if (!OurTurn) return null;
            
            if (!this.Ai.MemoryService.PlayersInitialized)
                return Messages.PlayersInfo;

            if (!this.Ai.MemoryService.RoadInitialized)
                return Messages.RoutesInfo;
            
            return WorkForAction(lastReceivedMessage);  
        }
    }

    public virtual string? WorkForAction(string lastReceivedMessage)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// fonction commune que l'on doit lancer à tous les debuts de tour
    /// </summary>
    public string? StartTurn(string lastReceivedMessage)
    {
            // si on a les infos sur les joueurs et les routes
            return null;
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