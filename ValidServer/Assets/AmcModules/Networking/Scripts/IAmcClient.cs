/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
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