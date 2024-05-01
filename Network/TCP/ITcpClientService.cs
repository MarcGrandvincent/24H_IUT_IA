namespace Network.TCP;

public interface ITcpClientService
{
    /// <summary>
    /// Envoie un message.
    /// </summary>
    /// <param name="message">Le message à envoyer.</param>
    public void SendMessage(string message);
    
    /// <summary>
    /// Reçoie un message.
    /// </summary>
    public string GetMessage();

    /// <summary>
    /// Ferme la connexion avec le serveur.
    /// </summary>
    public void CloseConnection();
}