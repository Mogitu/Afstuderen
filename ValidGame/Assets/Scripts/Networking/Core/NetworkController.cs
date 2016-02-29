using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Abstract controller with basic implementations for setting up context types.
/// </summary>
namespace AMC.Networking
{
    public abstract class NetworkController : MonoBehaviour, INetworkController
    {
        protected INetworkContext NetworkContext;
        protected IAmcServer AmcServer;
        protected IAmcClient AmcClient;

        protected bool IsClient
        {
            get;
            set;
        }     

        protected void CreateServerContext<T>(int socketPort) where T : IAmcServer, new()
        {
            AmcServer = new T();
            AmcServer.SocketPort = socketPort;
            NetworkContext = AmcServer;        
        }

        protected void CreateClientContext<T>(string ip, int socketPort) where T : IAmcClient, new()
        {
            AmcClient = new T();
            AmcClient.IpAdress = ip;
            AmcClient.SocketPort = socketPort;
            NetworkContext = AmcClient;
        }     

        public abstract void AddHandlers();       
    }
}