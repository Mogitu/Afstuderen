using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Networking
{
    public class AmcServer:IAmcServer
    {     
        private int SocketPort;

        public AmcServer()
        {
            SocketPort = 7777;
            NetworkServer.Listen(SocketPort);
        }

        public AmcServer( int socketPort)
        {            
            SocketPort = socketPort;
            NetworkServer.Listen(SocketPort);
        }

        public void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage)
        {
            NetworkServer.RegisterHandler(msgType, networkMessage);
        }

        public void SendMessage(short msgType, MessageBase msgs)
        {
         NetworkServer.SendToAll(msgType, msgs);
        }

        public int ConnectionCount
        {
            get { return NetworkServer.connections.Count; }
        }
    }
}
