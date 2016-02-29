using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Abstract controller with basic implementations for setting up context types.
/// </summary>
namespace AMC.Networking
{
    public abstract class NetworkController : MonoBehaviour, INetworkController
    {
        private INetworkContext NetworkContext;
        private IAmcServer AmcServer;
        private IAmcClient AmcClient;

        protected bool IsClient { get; set; }

        protected abstract void AddHandlers();
        public abstract void StartHosting();
        public abstract void StartClient(string ip);

        /// <summary>
        /// Create a NEW server context
        /// </summary>
        /// <typeparam name="T">The type of server to create. This must implement the IAmcServer interface.</typeparam>
        /// <param name="socketPort">Listen to this port</param>
        protected void CreateServerContext<T>(int socketPort) where T : IAmcServer, new()
        {
            AmcServer = new T();
            AmcServer.SocketPort = socketPort;
            NetworkContext = AmcServer;
        }

        /// <summary>
        /// Create a NEW client context
        /// </summary>
        /// <typeparam name="T">The type of client to create. This must implement the IAmcClient interface.</typeparam>
        /// <param name="ip">Connect to this ip</param>
        /// <param name="socketPort">Listen to this port</param>
        protected void CreateClientContext<T>(string ip, int socketPort) where T : IAmcClient, new()
        {
            AmcClient = new T();
            AmcClient.IpAdress = ip;
            AmcClient.SocketPort = socketPort;
            NetworkContext = AmcClient;
        }

        public void SendMessage(short msgType, MessageBase msg)
        {
            NetworkContext.SendMessage(msgType, msg);
        }

        public void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            NetworkContext.RegisterHandler(msgType, networkMessage);
        }
    }
}