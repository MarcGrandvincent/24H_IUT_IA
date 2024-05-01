namespace _24H_IUT_IA.AI.Services;

public class ReactionService
{
    public ReactionService(AI ai)
    {
        Ai = ai;
    }
    
    /// <summary>
    /// L'IA en cours.
    /// </summary>
    public AI Ai { get; set; }
    
    /// <summary>
    /// Réaction au message reçu.
    /// </summary>
    /// <param name="sentMessage">Le dernier message envoyé.</param>
    /// <param name="receivedMessage">Le dernier message reçu.</param>
    public void ReactToMessage(string sentMessage, string receivedMessage)
    {
    }
}