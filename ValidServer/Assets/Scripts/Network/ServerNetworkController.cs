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

        EventManager.AddListener(ServerEvents.StartServer,Begin);
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
        for (short i = MsgType.Highest; i < MsgType.Highest + 20; i++)
        {
            RegisterHandler(i, Relay);
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
        }else if(match.ConnectionB ==0)
        {
            match.ConnectionB = msg.conn.connectionId;
        }        
    }


    protected override void OnDisconnect(NetworkMessage msg)
    {
        base.OnDisconnect(msg);
        ConnectionIds.Remove(msg.conn.connectionId);
    }


    private void Relay(NetworkMessage msg)
    {
        Match match= null;
        int connId = msg.conn.connectionId;
        //find correct match by basis of connection id
        for(int i=0;i < Matches.Count; i++)
        {
            if (Matches[i].ConnectionA == msg.conn.connectionId || Matches[i].ConnectionB == msg.conn.connectionId)
            {
                match = Matches[i];
                break;
            }
        }
     
        MessageBase msgBase = msg.ReadMessage<ChatMessage>();

        /*
        if(msg.ReadMessage<ChatMessage>() != null)
        {
            msgBase = msg.ReadMessage<ChatMessage>();
        }

       
        if (msg.ReadMessage<ScoreMessage>() != null)
        {
            msgBase = msg.ReadMessage<ScoreMessage>();
        }

        if (msg.ReadMessage<PlayerLeftMessage>() != null)
        {
            msgBase = msg.ReadMessage<PlayerLeftMessage>();
        }

        if (msg.ReadMessage<PlayerJoinedMessage>() != null)
        {
            msgBase = msg.ReadMessage<PlayerJoinedMessage>();
        }

        if (msg.ReadMessage<CardToOpponentMessage>() != null)
        {
            msgBase = msg.ReadMessage<CardToOpponentMessage>();
        }
        */
        if (connId == match.ConnectionA)
        {            
            NetworkServer.SendToClient(match.ConnectionB, msg.msgType,msgBase);
        }else if(connId == match.ConnectionB)
        {
            NetworkServer.SendToClient(match.ConnectionA, msg.msgType, msgBase);
        }        
    }
}