using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default controller with basic functionality that should carry over to every application
/// </summary>
namespace AMC.Networking
{
    public class StandardNetworkController : NetworkController
    {
        public int SocketPort = 7777;
        // Use this for initialization
        void Start()
        {

        }    

        public override void StartClient(string ip)
        {
            CreateClientContext<AmcClient>(ip, SocketPort);
            AddHandlers();
            IsClient = true;
        }

        public override void StartHosting()
        {
            CreateServerContext<AmcServer>(SocketPort);
            AddHandlers();
            IsClient = false;
        }

        protected override void AddHandlers()
        {
            
        }    
    }
}