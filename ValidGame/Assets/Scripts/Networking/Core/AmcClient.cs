using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :
/// </summary>
namespace AMC.Networking
{
    public class AmcClient : IAmcClient
    {
        private NetworkClient Client;
        private string IpAdress;
        private int SocketPort;        

        public AmcClient()
        {
            IpAdress = "127.0.0.1";
            SocketPort = 7777;

            Client = new NetworkClient();
            Client.Connect(IpAdress, SocketPort);
        }

        public AmcClient(string ipAdress, int socketPort)
        {
            IpAdress = ipAdress;
            SocketPort = socketPort;

            Client = new NetworkClient();
            Client.Connect(IpAdress, SocketPort);
        }
     

        public void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            Client.RegisterHandler(msgType, networkMessage);
        }

        public void SendMessage(short msgType, MessageBase msgs)
        {
            Client.Send(msgType, msgs);
        }         
    }
}
