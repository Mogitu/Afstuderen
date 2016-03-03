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
        public int SocketPort = 7777;
        public string IpAdress = "127.0.0.1";

        private INetworkContext NetworkContext;
        private IAmcServer AmcServer;
        private IAmcClient AmcClient;

        protected bool IsClient { get; set; }

        protected abstract void AddHandlers();
        public abstract void StartHosting();
        public abstract void StartClient(string ipAdress);
        public abstract void StartClient();

        /// <summary>
        /// Create a NEW server context
        /// </summary>
        /// <typeparam name="T">The type of server to create. This must implement the IAmcServer interface.</typeparam>
        /// <param name="socketPort">Listen to this port</param>
        protected virtual void CreateServerContext<T>(int socketPort) where T : IAmcServer, new()
        {
            AmcServer = new T();
            AmcServer.SocketPort = socketPort;
            NetworkContext = AmcServer;
            IsClient = false;
            AddInternalHandlers();
            AddHandlers();
        }

        protected virtual void CreateServerContext<T>() where T : IAmcServer, new()
        {
            CreateServerContext<T>(SocketPort);
        }

        /// <summary>
        /// Create a NEW client context
        /// </summary>
        /// <typeparam name="T">The type of client to create. This must implement the IAmcClient interface.</typeparam>
        /// <param name="ip">Connect to this ip</param>
        /// <param name="socketPort">Listen to this port</param>
        protected virtual void CreateClientContext<T>(string ip, int socketPort) where T : IAmcClient, new()
        {
            AmcClient = new T();
            AmcClient.IpAdress = ip;
            AmcClient.SocketPort = socketPort;
            NetworkContext = AmcClient;
            IsClient = true;
            AddInternalHandlers();
            AddHandlers();
        }

        protected virtual void CreateClientContext<T>() where T : IAmcClient, new()
        {
            CreateClientContext<T>(IpAdress, SocketPort);
        }

        public virtual void SendNetworkMessage(short msgType, MessageBase msg)
        {
            NetworkContext.SendNetworkMessage(msgType, msg);
        }

        public virtual void SendNetworkMessage(short msgType)
        {
            NetworkContext.SendNetworkMessage(msgType);
        }

        public virtual void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            NetworkContext.RegisterHandler(msgType, networkMessage);
        }

        public virtual void Disconnect()
        {
            NetworkContext.Disconnect();
        }

        private void AddInternalHandlers()
        {
            RegisterHandler(MsgType.Connect, OnConnectionReceived);
            RegisterHandler(MsgType.Disconnect, OnDisconnect);
        }

        protected virtual void OnConnectionReceived(NetworkMessage msg)
        {
            Debug.Log("Connection received.");
        }

        protected virtual void OnDisconnect(NetworkMessage msg)
        {
            Debug.Log("Connection left.");
        }
    }
}