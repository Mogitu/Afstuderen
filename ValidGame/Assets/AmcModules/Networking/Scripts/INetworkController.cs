/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
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