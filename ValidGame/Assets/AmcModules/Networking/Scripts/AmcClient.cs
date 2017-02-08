using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Client that connect to an AmcServer
/// TODO    :   The current network Implementation is probably outdated as it uses Unity 5.1 experimental features.
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
            //SetupConnection("127.0.0.1", 7777);
        }

        public AmcClient(string ipAdress, int socketPort)
        {
            SetupConnection(IpAdress, SocketPort);
        }

        public void SetupConnection(string ipAdress, int socketPort)
        {
            Client = new NetworkClient();
            IpAdress = ipAdress;
            SocketPort = socketPort;
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
