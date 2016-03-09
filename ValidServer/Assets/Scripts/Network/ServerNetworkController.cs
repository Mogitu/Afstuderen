using UnityEngine;
using AMC.Networking;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
public class ServerNetworkController : NetworkController
{
    [SerializeField]
    private EventManager EventManager;
    private List<int> ConnectionIds;
    private List<Match> Matches;

    void Start()
    {
        ConnectionIds = new List<int>();
        Matches = new List<Match>();
        EventManager.AddListener(ServerEvents.StartServer, Begin);
        EventManager.AddListener(ServerEvents.Disconnect, OnDisconnect);
        EventManager.AddListener(ServerEvents.QuitApplication, QuitApplication);
    }  

    public override void StartHosting()
    {
        CreateServerContext<AmcServer>();
    }

    private void Begin(short event_Type, Component sender, object param = null)
    {
        StartHosting();
    }

    protected override void AddHandlers()
    {
        RegisterHandler(NetworkMessages.MsgChat, Relay<ChatMessage>);   
        RegisterHandler(NetworkMessages.MsgPlayerLeft, Relay<PlayerLeftMessage>);
        RegisterHandler(NetworkMessages.MsgScore, Relay<ScoreMessage>);
        RegisterHandler(NetworkMessages.OpponentCard, Relay<CardToOpponentMessage>);
    }     

    private void Relay<T>( NetworkMessage msg) where T : MessageBase, new()
    {
        MessageBase msgBase = msg.ReadMessage<T>();
        Match match = null;
        int connId = msg.conn.connectionId;
        //find correct match by basis of connection id
        for (int i = 0; i < Matches.Count; i++)
        {
            if (Matches[i].ConnectionA == msg.conn.connectionId || Matches[i].ConnectionB == msg.conn.connectionId)
            {
                match = Matches[i];
                break;
            }
        }
        if (connId == match.ConnectionA)
        {
            NetworkServer.SendToClient(match.ConnectionB, msg.msgType, msgBase);
        }
        else if (connId == match.ConnectionB)
        {
            NetworkServer.SendToClient(match.ConnectionA, msg.msgType, msgBase);
        }
    }

    protected override void OnConnectionReceived(NetworkMessage msg)
    {
        base.OnConnectionReceived(msg);
        ConnectionIds.Add(msg.conn.connectionId);
        Match match = null;
        if (Matches.Count < 1)
        {
            match = new Match();
            Matches.Add(match);
            EventManager.PostNotification(ServerEvents.MatchCreated, this, null);
        }
        else
        {
            match = Matches[Matches.Count - 1];
            if (match.ConnectionA != 0 && match.ConnectionB != 0)
            {
                match = new Match();
                Matches.Add(match);
                EventManager.PostNotification(ServerEvents.MatchCreated, this, null);
            }
        }

        if (match.ConnectionA == 0)
        {
            match.ConnectionA = msg.conn.connectionId;
        }
        else if (match.ConnectionB == 0)
        {
            match.ConnectionB = msg.conn.connectionId;
        }
        EventManager.PostNotification(ServerEvents.ClientJoined, this, null);
    }

    protected override void OnDisconnect(NetworkMessage msg)
    {
        base.OnDisconnect(msg);
        ConnectionIds.Remove(msg.conn.connectionId);
    }

    public override void Disconnect()
    {
        base.Disconnect();
        NetworkServer.Shutdown();
    }

    private void OnDisconnect(short eventType, Component sender, object param = null)
    {
        Disconnect();
    }

    public void QuitApplication(short eventType, Component sender, object param=null)
    {
        Application.Quit();
    }


    public override void StartClient()
    {

    }

    public override void StartClient(string ipAdress)
    {

    }
}