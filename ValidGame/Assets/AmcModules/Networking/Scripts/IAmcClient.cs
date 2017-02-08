/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// TODO    :   The current network Implementation is probably outdated as it uses Unity 5.1 experimental features.
/// </summary>
namespace AMC.Networking
{
    public interface IAmcClient : INetworkContext
    {
        string IpAdress { get; set; }
        int SocketPort { get; set; }
        void SetupConnection(string ipAdress, int socketPort);
    }
}