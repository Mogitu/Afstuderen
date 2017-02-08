using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :
/// TODO    :   The current network Implementation is probably outdated as it uses Unity 5.1 experimental features.
/// </summary>
namespace AMC.Networking
{
    public interface INetworkContext
    {
        void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage);
        void SendNetworkMessage(short msgType, MessageBase msgs);
        void SendNetworkMessage(short msgType);
        void Disconnect();
    }
}