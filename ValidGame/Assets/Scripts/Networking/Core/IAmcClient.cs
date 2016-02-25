using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Networking
{
    public interface IAmcClient
    {
        void RegisterHandler(short msgType, NetworkMessageDelegate networkMessage);
        void SendMessage(short msgType, MessageBase msgs);
    }
}

