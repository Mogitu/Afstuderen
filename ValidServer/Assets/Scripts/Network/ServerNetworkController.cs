using UnityEngine;
using AMC.Networking;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Rather than a "gamemanager" this class controls the state of the program. Done like this because the only thing this application does is "controlling" network states.
/// TODO    :   Most methods can be improved a lot, especially where there are comparisons done between connections in a match.
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
        RegisterHandler(NetworkMessages.MsgScore, Relay<ScoreMessage>);        
        RegisterHandler(NetworkMessages.OpponentCard, Relay<CardToOpponentMessage>);       
    }     

    /// <summary>
    /// Relays message of type T to other client in a a match.
    /// </summary>
    /// <typeparam name="T">The type of message to relay, needs to inherit from MessageBase</typeparam>
    /// <param name="msg">Obligated paramater in order to properly handle message parsing in the handlers</param>
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
  
    private Match FindMatch()
    {
        Match match = null;
        if (Matches.Count < 1)
        {
            match = CreateMatch();
        }
        else
        {
            match = Matches[Matches.Count - 1];
            if (match.ConnectionA != 0 && match.ConnectionB != 0)
            {
                match = CreateMatch();
            }
        }
        return match;
    }

    /// <summary>
    /// Creates a new match and adds it to the Matches list
    /// </summary>
    /// <returns>The newly created match</returns>
    private Match CreateMatch()
    {
        Match match = new Match();
        Matches.Add(match);       
        EventManager.PostNotification(ServerEvents.MatchCreated, this, GetStats());
        return match;
    }    

    protected override void OnConnectionReceived(NetworkMessage msg)
    {
        base.OnConnectionReceived(msg);
        ConnectionIds.Add(msg.conn.connectionId);
        Match match = FindMatch();

        if (match.ConnectionA == 0)
        {
            match.ConnectionA = msg.conn.connectionId;
        }
        else if (match.ConnectionB == 0)
        {
            match.ConnectionB = msg.conn.connectionId;
            TeamTypeMessage teamMsg = new TeamTypeMessage();
            teamMsg.TeamType = 1;
            NetworkServer.SendToClient(match.ConnectionA, NetworkMessages.MsgTeamType, teamMsg);
            teamMsg.TeamType = 2;
            NetworkServer.SendToClient(match.ConnectionB, NetworkMessages.MsgTeamType, teamMsg);
        }       
        EventManager.PostNotification(ServerEvents.ClientJoined, this, GetStats());
    }

    private int[] GetStats()
    {
        int[] stats = { ConnectionIds.Count, Matches.Count };
        return stats;
    }

    protected override void OnDisconnect(NetworkMessage msg)
    {
        base.OnDisconnect(msg);        
        foreach(Match match in Matches)
        {
            if (msg.conn.connectionId == match.ConnectionA || msg.conn.connectionId == match.ConnectionB)
            {                
                if (msg.conn.connectionId == match.ConnectionA)
                {
                    NetworkServer.SendToClient(match.ConnectionB, NetworkMessages.MsgPlayerLeft, new PlayerLeftMessage());
                }
                else
                {
                    NetworkServer.SendToClient(match.ConnectionA, NetworkMessages.MsgPlayerLeft, new PlayerLeftMessage());
                }
                ConnectionIds.Remove(match.ConnectionA);
                ConnectionIds.Remove(match.ConnectionB);               
                Matches.Remove(match);
                break;
            }            
        }        
        EventManager.PostNotification(ServerEvents.PlayerLeft, this, GetStats());              
    }

    public override void Disconnect()
    {
        base.Disconnect();
        NetworkServer.Shutdown();
    }

    private void OnServerDisconnect(short eventType, Component sender, object param = null)
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