using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Networking
{
    public abstract class NetworkController: MonoBehaviour, INetworkController
    {
        public abstract void BeginHosting();
        public abstract void StartClient(string ip);
        public abstract void AddListeners();
        public abstract void ReceiveMessage();
        public abstract void SendMessage();
        public abstract void Disconnect();
    }
}
