/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   ...
/// </summary>

namespace AMC.Networking
{
    public interface IAmcServer:INetworkContext
    {
       int SocketPort { get; set; }
       void DisconnectedClient(int connectionId);   
          
    }
}
