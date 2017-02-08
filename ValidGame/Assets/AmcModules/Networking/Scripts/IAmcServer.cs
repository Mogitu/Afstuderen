/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   ...
/// TODO    :   The current network Implementation is probably outdated as it uses Unity 5.1 experimental features.
/// </summary>
namespace AMC.Networking
{
    public interface IAmcServer : INetworkContext
    {
        int SocketPort { get; set; }
        void DisconnectedClient(int connectionId);
        void SetupListening(int socketPort);
    }
}
