using UnityEngine;
using AMC.Networking;
using UnityEngine.Networking;
using System.Collections.Generic;

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
    }

    public override void StartClient()
    {

    }

    public override void StartClient(string ipAdress)
    {

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
        RegisterHandler(NetworkMessages.MsgChat, OnChat);
        RegisterHandler(NetworkMessages.MsgPlayerJoined, OnPlayerJoined);
        RegisterHandler(NetworkMessages.MsgPlayerLeft, OnPlayerLeft);
        RegisterHandler(NetworkMessages.MsgScore, OnScore);
        RegisterHandler(NetworkMessages.OpponentCard, OnOpponentCard);
    }

    private void OnChat(NetworkMessage msg)
    {
        ChatMessage msgBase = msg.ReadMessage<ChatMessage>();
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

    private void OnPlayerJoined(NetworkMessage msg)
    {
        PlayerJoinedMessage msgBase = msg.ReadMessage<PlayerJoinedMessage>();
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

    private void OnPlayerLeft(NetworkMessage msg)
    {
        PlayerLeftMessage msgBase = msg.ReadMessage<PlayerLeftMessage>();
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

    private void OnScore(NetworkMessage msg)
    {
        ScoreMessage msgBase = msg.ReadMessage<ScoreMessage>();
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

    private void OnOpponentCard(NetworkMessage msg)
    {
        CardToOpponentMessage msgBase = msg.ReadMessage<CardToOpponentMessage>();
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
        }
        else
        {
            match = Matches[Matches.Count - 1];
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

    private void Relay<T>(short msgType, NetworkMessage msg) where T : MessageBase
    {

    }
}