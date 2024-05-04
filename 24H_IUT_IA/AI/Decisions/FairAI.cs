using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

/// <summary>
///     This class represents a decision-making service for an AI.
///     It contains methods for determining the best action to take based on the current state of the game.
/// </summary>
public class FairAi(AI ai) : DecisionMakingService(ai)
{
    //tuple qui stock le score d'attaque et la route à attaquer
    public int[] AttackScore { get; set; } = [0, 0, 0];

    //tuple qui stock le score d'une trahison et le joueur à trahir
    public int[] BetrayScore { get; set; } = [0, 0, 0];

    //int qui stock le score de réparer
    public int RepareScore { get; set; }

    //int qui stock le score de vendre
    public int ResellScore;

    /// <summary>
    ///     Logic for the Fair AI.
    /// </summary>
    /// <param name="lastReceivedMessage">The last message received from the game server.</param>
    /// <returns>The action to take as a string, or null if no action should be taken.</returns>
    public override string? WorkForAction(string lastReceivedMessage)
    {
        while (lastReceivedMessage != "OK")
            if (Orders.Count == 0)
            {
                //remplis toutes les auto property avec les scores des actions
                var (road, scoreAttack, numberUpdgradeAttack) = TestRoute();
                AttackScore[0] = road;
                AttackScore[1] = scoreAttack;
                AttackScore[2] = numberUpdgradeAttack;

                var (playerToAttack, scoreBetray, numberUpgradeBetray) = BetrayGen();
                BetrayScore[0] = playerToAttack;
                BetrayScore[1] = scoreBetray;
                BetrayScore[2] = numberUpgradeBetray;

                RepareScore = RepareGen();
                ResellScore = ResellGen();

                //test de tous les scores 
                var score = ((int[]) [AttackScore[1], BetrayScore[1], ResellScore, RepareScore]).Max(); 
                if (score == RepareScore)
                {
                    Orders.Add(Messages.Repair);
                }
                else if (score == ResellScore)
                {
                    Orders.Add(Messages.Fence);
                }
                else if (score == scoreAttack)
                {
                    for (var i = 0; i < numberUpdgradeAttack; i++) Orders.Add(Messages.Recruit);
                    Orders.Add(Pillage(road));
                }
                else if (score == scoreBetray)
                {
                    for (var i = 0; i < numberUpgradeBetray; i++) Orders.Add(Messages.Recruit);
                    Orders.Add(Betray(playerToAttack));
                }
             
            }
            else
            {
                var response = Orders.First();
                Orders.Remove(response);
                return response;
            }

        OurTurn = false;
        return null;
    }


    /// <summary>
    ///     réparer le bateau dans le cas général
    /// </summary>
    /// <returns></returns>
    public int RepareGen()
    {
        if (Ai.MemoryService.GetOurPlayerInfo().Life > 3 || Ai.MemoryService.Turn == 120)
            return 0;
        return int.MaxValue - 1;
        //si on a plus de 3(ou 2(à tester)) pv ou si c'est le detrnier tour on renvoie 0 sinon on renvoie 100
        return 0;
    }

    /// <summary>
    ///     détermine si on doit revendre nos coffres dans un cas général
    /// </summary>
    /// <returns></returns>
    public int ResellGen()
    {
        //si notre liste de coffres Chests est vide on renvoie 0
        if (Ai.MemoryService.GetOurPlayerInfo().Chests.Count == 0)
            return 0;

        // sinon si Chests.Count() = 5 (si on a le max de coffres) ou si c'est le dernier tour on renvoie 100
        if (Ai.MemoryService.GetOurPlayerInfo().Chests.Count == 5 || Ai.MemoryService.Turn == 120)
            return int.MaxValue;

        // sinon => le butin général * (1/2 + l'ordre du joueur/8)
        return (int)(Ai.MemoryService.GetOurPlayerInfo().LootValue *
                     (0.5 + Ai.MemoryService.GetOurPlayerInfo().Order / 8));
    }

    /// <summary>
    ///     détermine si on doit trahir un joueur dans un cas général
    /// </summary>
    public (int, int, int) BetrayGen()
    {
        var ret = (0, 0, 0);
        var tmpNumjoueur = 0;
        foreach (var player in Ai.MemoryService.Players)
        {
            if (player.CanBeBetray(Ai.MemoryService.GetOurPlayerInfo()))
            {
                var ourAttackValue = Ai.MemoryService.GetOurPlayerInfo().AttackValue;
                var tempUpgrade = 0;
                while (ourAttackValue < player.AttackValue)
                {
                    tempUpgrade++;
                    ourAttackValue += 5;
                }

                if (EstTuable(player) && ret.Item2 <
                    player.Chests[0] + 2 * player.Chests[1] + 2000 +
                    (player.AttackValue - 5) * 100 )
                {
                    ret.Item1 = tmpNumjoueur;
                    ret.Item2 = player.Chests[0] + 2 * player.Chests[1] + 2000 +
                                (player.AttackValue - 5) * 100 ;
                    ret.Item3 = tempUpgrade;
                }
                else if (ret.Item2 < player.Chests[0] + 2 * player.Chests[1])
                {
                    ret.Item1 = tmpNumjoueur;
                    ret.Item2 = player.Chests[0] + 2 * player.Chests[1] ;
                    ret.Item3 = tempUpgrade;
                }
            }

            tmpNumjoueur++;
        }

        return ret;
    }


    public bool EstTuable(Player player)
    {
        var ret = false;
        var route = player.Activity switch
        {
            ActivityEnum.PILLER1 => 1,
            ActivityEnum.PILLER2 => 2,
            ActivityEnum.PILLER3 => 3,
            ActivityEnum.PILLER4 => 4,
            _ => 0
        };
        if (!Ai.MemoryService.Roads[route].IsMonsterPresent)
            switch (player.Life)
            {
                case 1:
                    ret = true;
                    break;
                case 2:
                    var minOrder = 10;
                    foreach (var j in Ai.MemoryService.Players)
                        if (j.Activity == player.Activity)
                            if (j.Order < player.Order && j.Order != player.Order && j.Order < minOrder)
                                minOrder = j.Order;
                    if (player.Order == minOrder)
                    {
                        ret = true;
                    }

                    break;
            }

        return ret;
    }


    /// <summary>
    ///     Tests a route for potential attack.
    /// </summary>
    /// <returns>A tuple containing the score for the attack action and the route to attack.</returns>
    public (int, int, int) TestRoute()
    {
        // Item1 = route à attaquer
        // Item2 = score de la route
        // Item3 = nombre d'upgrade à faire
        var ret = (0, 0, 0);
        var ourAttackValue = Ai.MemoryService.GetOurPlayerInfo().AttackValue;
        foreach (var route in Ai.MemoryService.Roads)
            if (route.AttackValue <= ourAttackValue + (Ai.MemoryService.GetOurPlayerInfo().Score / 500) * 5)
            {
               int tempUpgrade = 0;
                while (route.AttackValue > ourAttackValue)
                {
                    tempUpgrade++;
                    ourAttackValue += 5;
                }

                if (ret.Item2 < MaxChest(Ai.MemoryService.Roads.IndexOf(route) + 1))
                {
                    ret.Item2 = MaxChest(Ai.MemoryService.Roads.IndexOf(route) + 1);
                    ret.Item1 = Ai.MemoryService.Roads.IndexOf(route) + 1;
                    ret.Item3 = tempUpgrade;
                }
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
        var counter = 0;
        var res = 0;
        foreach (var joueur in Ai.MemoryService.Players)
            if (joueur.Activity.ToString() == $"PILLER{nbRoad}")
                counter++;

        if (counter < 3)
            switch (counter + 1)
            {
                case 1:
                    res = Ai.MemoryService.Roads[nbRoad - 1].ChestValue1;
                    break;
                case 2:
                    res = Ai.MemoryService.Roads[nbRoad - 1].ChestValue2;
                    break;
                case 3:
                    res = Ai.MemoryService.Roads[nbRoad - 1].ChestValue3;
                    break;
            }

        if (Ai.MemoryService.Roads[nbRoad - 1].IsMonsterPresent)
            res = (int)(res * 0.4);

        return res;
    }
}