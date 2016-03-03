using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Networking
{
    public class AmcServer : IAmcServer
    {
        public int SocketPort { get; set; }

        public AmcServer()
        {
            SocketPort = 7777;
            Setup(SocketPort);
        }

        public AmcServer(int socketPort)
        {
            SocketPort = socketPort;
            Setup(socketPort);
        }

        private void Setup(int socketPort)
        {
            NetworkServer.Listen(socketPort);
        }

        public void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            NetworkServer.RegisterHandler(msgType, networkMessage);
        }

        public void SendNetworkMessage(short msgType, MessageBase msgs)
        {
            NetworkServer.SendToAll(msgType, msgs);
        }

        public void SendNetworkMessage(short msgType)
        {
            NetworkServer.SendToAll(msgType, new IntegerMessage());
        }

        public void Disconnect()
        {
            NetworkServer.DisconnectAll();           
        }

        public void DisconnectedClient(int connectionID)
        {            
            NetworkServer.connections[connectionID].Disconnect();
        }       

        public int ConnectionCount
        {
            get { return NetworkServer.connections.Count; }
        }
    }
}
