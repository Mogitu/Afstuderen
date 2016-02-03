public interface INetworkController
{
    void BeginHosting();
    void StartClient(string ip);
    void AddListeners();
    void ReceiveMessage();
    void SendMessage();
    void Disconnect();   
}

