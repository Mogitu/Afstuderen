/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// TODO    :   The current network Implementation is probably outdated as it uses Unity 5.1 experimental features.
/// </summary>
/// 
namespace AMC.Networking
{
    public interface INetworkController : INetworkContext
    {
        void StartHosting();
        void StartClient();
        void StartClient(string ipAdress);
    }
}