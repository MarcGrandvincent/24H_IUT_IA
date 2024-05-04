﻿using _24H_IUT_IA.AI.Services;

namespace _24H_IUT_IA.AI.Decisions;

public class FairAI(AI ai) : DecisionMakingService(ai)
{
    // définition de 4 scores pour chaque action
    public int RepareScore { get; set; }
    public int RecellScore { get; set; }
    public int BetrayScore { get; set; }
    public int AttackScore { get; set; }
    
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
    /// logic de flair ai 
    /// </summary>
    /// <param name="lastReceivedMessage">dernier message reçu</param>
    /// <returns></returns>
    private string? WorkForAction(string lastReceivedMessage)
    {
        return lastReceivedMessage;
    }

    /// <summary>
    /// réparer le bateau dans le cas général
    /// </summary>
    /// <returns></returns>
    public int RepareGen()
    {
        //si on a plus de 3(ou 2(à tester)) pv ou si c'est le detrnier tour on renvoie 0 sinon on renvoie 100
        return 0;
    }

    /// <summary>
    /// détermine si on doit revendre nos coffres dans un cas général
    /// </summary>
    /// <returns></returns>
    public int RecellGen()
    {
        //si notre liste de coffres Chests est vide on renvoie 0
        // sinon si Chests.Count() = 5 (si on a le max de coffres) ou si c'est le dernier tour on renvoie 100
        // sauf si dans le dernier tour on a 0 coffres on renvoie 0
        // sinon => le butin général * (1/2 + l'ordre du joueur/8)
        return 0;
    }
    
    /// <summary>
    /// détermine si on doit trahir un joueur dans un cas général
    /// </summary>
    public int BetrayGen()
    {
        //
        return 0;
    }
    
    /// <summary>
    /// détermine si on doit attaquer une route commerciale
    /// </summary>
    public int Attack()
    {
        return 0;
    }
}