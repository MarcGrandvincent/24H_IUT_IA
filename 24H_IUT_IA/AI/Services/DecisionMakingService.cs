namespace _24H_IUT_IA.AI.Services;

public abstract class DecisionMakingService
{
    /// <summary>
    /// L'IA en cours.
    /// </summary>
    public AI Ai { get; set; }
    
    protected DecisionMakingService(AI ai)
    {
        Ai = ai;
    }

    
    /// <summary>
    /// Determine une action à prendre.
    /// </summary>
    /// <param name="lastReceivedMessage"></param>
    /// <returns></returns>
    public virtual string TakeNewAction(string lastReceivedMessage)
    {
        throw new NotImplementedException();
    }
}