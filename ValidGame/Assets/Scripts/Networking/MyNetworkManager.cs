using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   ...
/// </summary>
public class MyNetworkManager
{

    private int reliableChannelId;
    private int socketId;
    private int socketPort = 8888;
    private int connectionId;
    private string ipAdress;

    private ConnectionConfig config;
    private HostTopology topology;
    private const int maxConnections = 10;
    private MainManager manager;

    public MyNetworkManager(MainManager manager)
    {
        NetworkTransport.Init();
        config = new ConnectionConfig();
        reliableChannelId = config.AddChannel(QosType.Reliable);
        topology = new HostTopology(config, maxConnections);
        this.manager = manager;
    }

    //TODO  :   Switch is ugly and hard to maintain.
    public void Update()
    {
        int recHostId;
        int recConnectionId;
        int recChannelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recNetworkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event received");
                break;
            case NetworkEventType.DataEvent:                
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                string message = formatter.Deserialize(stream) as string;
                //eventManager.PostNotification(EVENT_TYPE.SEND, null, message);
                Debug.Log("incoming message event received: " + message);
                break;              
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }
    }


    public void CreateHost(string ip)
    {
        ipAdress = ip;
        //addhost actually justs starts a socket.
        socketId = NetworkTransport.AddHost(topology, socketPort);
        Debug.Log("Socket Open. SocketId is: " + socketId);
        byte error;
        connectionId = NetworkTransport.Connect(socketId, ip, socketPort, 0, out error);
    }

    public void CreateClient()
    {
        //addhost actually justs starts a socket.
        socketId = NetworkTransport.AddHost(topology, socketPort);
        Debug.Log("Socket Open. SocketId is: " + socketId);
        byte error;
        connectionId = NetworkTransport.Connect(socketId, ipAdress, socketPort, 0, out error);
    }

    public void SendSocketMessage(string msgs)
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, msgs);
        int bufferSize = 1024;
        NetworkTransport.Send(socketId, connectionId, reliableChannelId, buffer, bufferSize, out error);
    }

    public void Disconnect()
    {
        byte error;
        NetworkTransport.Disconnect(socketId, connectionId, out error);
    }
}
