using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :
/// </summary>
namespace AMC.Networking
{
    public class AmcClient : IAmcClient
    {
        private NetworkClient Client;
        public string IpAdress { get; set; }
        public int SocketPort { get; set; }

        public AmcClient()
        {
            IpAdress = "127.0.0.1";
            SocketPort = 7777;

            Setup(IpAdress, SocketPort);
        }

        public AmcClient(string ipAdress, int socketPort)
        {
            IpAdress = ipAdress;
            SocketPort = socketPort;

            Setup(IpAdress, SocketPort);

        }

        private void Setup(string ipAdress, int socketPort)
        {
            Client = new NetworkClient();
            Client.Connect(IpAdress, SocketPort);
        }

        public void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            Client.RegisterHandler(msgType, networkMessage);
        }

        public void SendNetworkMessage(short msgType, MessageBase msgs)
        {
            Client.Send(msgType, msgs);
        }

        public void SendNetworkMessage(short msgType)
        {
            Client.Send(msgType, new IntegerMessage());
        }

        public void Disconnect()
        {
            Client.Disconnect();
        }       
    }
}
