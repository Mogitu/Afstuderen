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
        private NetworkClient client;
        private string ipAdress;
        private int socketPort;

        public AmcClient()
        {

        }

        public AmcClient(string ipAdress, int socketPort)
        {
            this.ipAdress = ipAdress;
            this.socketPort = socketPort;
        }

        private void Init()
        {
            client = new NetworkClient();
            client.Connect(ipAdress, socketPort);
        }

        public void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            client.RegisterHandler(msgType, networkMessage);
        }

        public void SendMessage(short msgType, MessageBase msgs)
        {
            client.Send(msgType, msgs);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
