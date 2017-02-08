/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default controller with basic functionality that should carry over to every application
/// TODO    :   The current network Implementation is probably outdated as it uses Unity 5.1 experimental features.
/// </summary>
namespace AMC.Networking
{
    public class StandardNetworkController : NetworkController
    {
        public override void StartClient()
        {
            CreateClientContext<AmcClient>(IpAdress, SocketPort);
        }

        public override void StartClient(string ipAdress)
        {
            CreateClientContext<AmcClient>(ipAdress, SocketPort);
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