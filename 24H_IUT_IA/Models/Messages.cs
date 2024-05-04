namespace _24H_IUT_IA.Models;

public static class Messages
{
    public const string Start = "START";
    public const string End = "END";
    public const string TeamName = "NOM_EQUIPE";
    public const string Welcome = "Bonjour";
    public const string StartTurn = "DEBUT_TOUR";
    public const string EndGame = "FIN";
    public const string Command = "NomCommande";
    public const string Pillage = "PILLER";
    public const string Repair = "REPARER";
    public const string Fence = "RECELER";
    public const string Recruit = "RECRUTER";
    public const string Betray = "TRAHIR";
    public const string PlayersInfo = "JOUEURS";
    public const string RoutesInfo = "ROUTES";
    public const string Ok = "OK";
    public const string NotOk = "NOK";
    
    public static string[] ActionsDrunkenIa = new []
    {
        Repair,
        Fence,
        Recruit,
        $"{Pillage}|1",
        $"{Pillage}|2",
        $"{Pillage}|3",
        $"{Pillage}|4",
        $"{Betray}|1",
        $"{Betray}|2",
        $"{Betray}|3",
        $"{Betray}|4"
    };
}