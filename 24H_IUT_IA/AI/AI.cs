using _24H_IUT_IA.AI.Decisions;
using _24H_IUT_IA.AI.Services;
using Network.TCP;

namespace _24H_IUT_IA.AI;

public class AI
{
    public const string TeamName = "1P5F"; 
    
    /// <summary>
    /// Service de communication.
    /// </summary>
    public ITcpClientService CommunicationService { get; set; }
    
    /// <summary>
    /// Service de mémoire.
    /// </summary>
    public MemoryService MemoryService { get; set; }
    
    /// <summary>
    /// Le service de réaction.
    /// </summary>
    public ReactionService ReactionService { get; set; }
    
    /// <summary>
    /// Le service de prise de décision.
    /// </summary>
    public DecisionMakingService DecisionMakingService { get; set; }
    
    private bool _communicationDone = false;

    /// <summary>
    /// Constructeur de l'IA.
    /// </summary>
    public AI()
    {
        CommunicationService = new TcpClientService(1234);
        MemoryService = new MemoryService();
        ReactionService = new ReactionService(this);
        DecisionMakingService = new FairAi(this);
    }
    
    /// <summary>
    /// Lance l'IA.
    /// </summary>
    public void Start()
    {
        _communicationDone = false;
        var receivedMessage = "";
        var sentMessage = "";
        
        //Boucle de discussion
        while (!_communicationDone)
        {
            receivedMessage = CommunicationService.GetMessage();

            ReactionService.ReactToMessage(sentMessage, receivedMessage);

            sentMessage = DecisionMakingService.TakeNewAction(receivedMessage);

            if (sentMessage is not null)
                CommunicationService.SendMessage(sentMessage);
        }
        
        CommunicationService.GetMessage();
        CommunicationService.CloseConnection();
    }

    public void StopCommunication() => _communicationDone = true;
}