using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :
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