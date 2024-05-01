using System.Net.Sockets;

namespace Network.TCP;

public class TcpClientService : ITcpClientService
{
    private readonly TcpClient _client;
    private readonly StreamReader _inflow;
    private readonly StreamWriter _outflow;
    private readonly bool _getInflowLog;
    private readonly bool _getOutflowLog;

    /// <summary>
    /// Crée un service pour gérer les communications TCP.
    /// </summary>
    /// <param name="port">Le port de communication.</param>
    /// <param name="getInflowLog">Récupérer ou non les logs pour les messages entrants.</param>
    /// <param name="getOutflowLog">Récupérer ou non les logs pour les messages sortants.</param>
    public TcpClientService(int port, bool getInflowLog = true, bool getOutflowLog = true)
    {
        _getInflowLog = getInflowLog;
        _getOutflowLog = getOutflowLog;
        _client = new TcpClient("localhost", port);
        _inflow = new StreamReader(_client.GetStream());
        _outflow = new StreamWriter(_client.GetStream()) { AutoFlush = true };
    }
    
    public void SendMessage(string message)
    {
        if (_getOutflowLog)
            Console.WriteLine(">> " + message);
        
        _outflow.WriteLine(message);
    }

    public string GetMessage()
    {
        var message = _inflow.ReadLine() ?? "END-OF-STREAM";
        
        if (_getInflowLog)
            Console.WriteLine("<< " + message);
        
        return message;
    }

    public void CloseConnection()
    {
        _client.Close();
    }
}