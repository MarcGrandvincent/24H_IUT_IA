using _24H_IUT_IA.AI.Services;
using _24H_IUT_IA.Models;

namespace _24H_IUT_IA.AI.Decisions;

public class ExempleAI(AI ai) : DecisionMakingService(ai)
{
    public override string? WorkForAction(string lastReceivedMessage)
    {
        while (lastReceivedMessage != "OK")
        {
            if (Orders.Count == 0)
            {
                Console.WriteLine(this.Ai.MemoryService.GetOurPlayerInfo().NumberOfChests);
                if (this.Ai.MemoryService.GetOurPlayerInfo().NumberOfChests >= 3)
                {

                    Orders.Add(Messages.Fence);                    
                }
                else if (this.Ai.MemoryService.GetOurPlayerInfo().Score >= 500)
                {
                    Orders.Add(Messages.Recruit);
                    Orders.Add(Pillage(4));
                }
                else
                {
                    Orders.Add(Pillage(4));   
                }
            }
            else
            {
                var response = Orders.First();
                Orders.Remove(response);
                return response;
            }
        }
        
        OurTurn = false;
        return null;
    }
}