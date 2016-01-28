public interface INetworkController
{
    void StartHost();
    void StartClient(string ip);
    void DisConnect();
    void ReceiveMsgs();
    void SendMsgs();
}

