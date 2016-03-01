using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default controller with basic functionality that should carry over to every application
/// </summary>
namespace AMC.Networking
{
    public class StandardNetworkController : NetworkController
    {       
        // Use this for initialization
        void Start()
        {

        }    

        public override void StartClient(string ip)
        {
            CreateClientContext<AmcClient>(ip, SocketPort);            
        }

        public override void StartHosting()
        {
            CreateServerContext<AmcServer>(SocketPort);          
        }

        protected override void AddHandlers()
        {
            
        }    
    }
}