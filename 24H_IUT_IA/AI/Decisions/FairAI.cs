using _24H_IUT_IA.AI.Services;

namespace _24H_IUT_IA.AI.Decisions;

/// <summary>
///     This class represents a decision-making service for an AI.
///     It contains methods for determining the best action to take based on the current state of the game.
/// </summary>
public class FairAI(AI ai) : DecisionMakingService(ai)
{
    // Dictionary containing the score for each action and the key is name of the action
    public Dictionary<string, int> ListScore = new Dictionary<string, int>
    {
        {"BETRAY", 0},
        {"ATTACK", 0},
        {"REPAIRE", 0},
        {"RESELL", 0}
    };
    /// <summary>
    ///     Determines the next action to take based on the last received message.
    /// </summary>
    /// <param name="lastReceivedMessage">The last message received from the game server.</param>
    /// <returns>The action to take as a string, or null if no action should be taken.</returns>
    public override string? TakeNewAction(string lastReceivedMessage)
    {
        // Call the common method for all turn starts
        if (StartTurn(lastReceivedMessage) is not null || !OurTurn)
            return StartTurn(lastReceivedMessage);
        // Start of drunken ai 
        return WorkForAction(lastReceivedMessage);
    }

    /// <summary>
    ///     Logic for the Fair AI.
    /// </summary>
    /// <param name="lastReceivedMessage">The last message received from the game server.</param>
    /// <returns>The action to take as a string, or null if no action should be taken.</returns>
    private string? WorkForAction(string lastReceivedMessage)
    {
        //remplis le dictionnaire avec les données des 4 tests
        FillListScore();
        //retourne l'action avec le score le plus élevé
        return ListScore.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        
    }

    private void FillListScore()
    {
        ListScore["REPAIRE"] = RepareGen();
        ListScore["RESELL"] = ResellGen();
        (ListScore["BETRAY"], _) = BetrayGen();
        ListScore["ATTACK"] = TestRoute().Item2;
    }

    /// <summary>
    ///     Determines whether to repair the boat in a general case.
    /// </summary>
    /// <returns>The score for the repair action.</returns>
    public int RepareGen()
    {
        if (Ai.MemoryService.GetOurPlayerInfo().Life > 3 || Ai.MemoryService.Turn == 120)
            return 0;
        return 100;
    }

    /// <summary>
    ///     Determines whether to sell our chests in a general case.
    /// </summary>
    /// <returns>The score for the sell action.</returns>
    public int ResellGen()
    {
        if (Ai.MemoryService.GetOurPlayerInfo().Chests.Count == 0 || Ai.MemoryService.Turn == 0 ||
            Ai.MemoryService.Turn == 120)
            return 0;

        if (Ai.MemoryService.GetOurPlayerInfo().Chests.Count == 5 || Ai.MemoryService.Turn == 100)
            return 100;

        return (int)(Ai.MemoryService.GetOurPlayerInfo().LootValue *
                     (0.5 + Ai.MemoryService.GetOurPlayerInfo().Order / 8));
    }

    /// <summary>
    ///     Determines whether to betray a player in a general case.
    /// </summary>
    /// <returns>A tuple containing the score for the betray action and the player to betray.</returns>
    public (int, int) BetrayGen()
    {
        var ret = (0, 0);
        foreach (var player in Ai.MemoryService.Players)
            if (player.Order != Ai.MemoryService.GetOurPlayerInfo().Order)
            {
            }

        return (0, 0);
    }
    

    /// <summary>
    ///     Tests a route for potential attack.
    /// </summary>
    /// <returns>A tuple containing the score for the attack action and the route to attack.</returns>
    public (int, int) TestRoute()
    {
        var ret = (0, 0);
        foreach (var route in Ai.MemoryService.Roads)
            if (route.AttackValue <= Ai.MemoryService.GetOurPlayerInfo().AttackValue)
                if (ret.Item2 < MaxChest(Ai.MemoryService.Roads.IndexOf(route) + 1))
                {
                    ret.Item2 = MaxChest(Ai.MemoryService.Roads.IndexOf(route) + 1);
                    ret.Item1 = Ai.MemoryService.Roads.IndexOf(route) + 1;
                }

        return ret;
    }

    /// <summary>
    ///     Returns the number of the route based on the route parameter.
    /// </summary>
    /// <param name="nbRoad">The route to test.</param>
    /// <returns>The maximum chest value for the route.</returns>
    public int MaxChest(int nbRoad)
    {
        var c = 0;
        var var = 0;
        foreach (var joueur in Ai.MemoryService.Players)
            if (joueur.Activity.ToString() == $"PILLER{nbRoad}")
                c++;

        if (c < 3)
            switch (c + 1)
            {
                case 1:
                    var = Ai.MemoryService.Roads[nbRoad].ChestValue1;
                    break;
                case 2:
                    var = Ai.MemoryService.Roads[nbRoad].ChestValue2;
                    break;
                case 3:
                    var = Ai.MemoryService.Roads[nbRoad].ChestValue3;
                    break;
            }

        if (Ai.MemoryService.Roads[nbRoad].IsMonsterPresent)
            var = (int)(var * 0.6);

        return var;
    }
}