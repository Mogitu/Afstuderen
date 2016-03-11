using UnityEngine;
using UnityEngine.Networking;
using AMC.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   High level networkmanager
/// </summary>

public class ValidNetworkController : NetworkController
{
    public EventManager EventManager;   

    void Start()
    {
        EventManager.AddListener(GameEvents.SendSchat, SendChatMsgs);
        EventManager.AddListener(GameEvents.SendScoreNetwork, SendScoreMsgs);
        EventManager.AddListener(GameEvents.SendCardToOpponent, SendOpponentCard);
    }

    public void SendOpponentCard(short Event_Type, Component Sender, object param = null)
    {
        CardToOpponentMessage msgA = (CardToOpponentMessage)param;
        SendNetworkMessage(NetworkMessages.OpponentCard, msgA);
    }

    public void OnOpponentCardReceived(NetworkMessage msg)
    {
        CardToOpponentMessage msgA = msg.ReadMessage<CardToOpponentMessage>();
        string name = msgA.CardName.Trim();
        GameObject go = GameObject.Find(name).gameObject;
        go.transform.position = msgA.Position;
        EventManager.PostNotification(GameEvents.CardReceivedFromOpponent, this, go);
    }

    public override void StartHosting()
    {
        string port = AmcUtilities.ReadFileItem("port", "config.ini").Trim();
        int.TryParse(port, out SocketPort);
        CreateServerContext<AmcServer>(SocketPort);               
    }

    public override void StartClient(string ip)
    {
        string port = AmcUtilities.ReadFileItem("port", "config.ini").Trim();
        int.TryParse(port , out SocketPort);
        CreateClientContext<AmcClient>(ip, SocketPort);              
    }

    public override void StartClient()
    {
        string port = AmcUtilities.ReadFileItem("port", "config.ini").Trim();
        int.TryParse(port, out SocketPort);
        CreateClientContext<AmcClient>(IpAdress, SocketPort);
    }

    private void SendChatMsgs(short event_Type, Component sender, object param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.Text = param.ToString();
        SendNetworkMessage(NetworkMessages.MsgChat, msgA);
    }

    private void SendScoreMsgs(short event_Type, Component sender, object param = null)
    {
        ScoreMessage msgA = new ScoreMessage();
        msgA.Score = (int)param;
        SendNetworkMessage(NetworkMessages.MsgScore, msgA);
    }

    private void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();
        EventManager.PostNotification(GameEvents.ReceiveChatNetwork, this, msgA.Text.ToString());
    }

    private void OnScoreMessageReceived(NetworkMessage msg)
    {
        ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();
        EventManager.PostNotification(GameEvents.ReceiveScoreNetwork, this, msgA.Score);
    }

    protected override void OnConnectionReceived(NetworkMessage msg)
    {
        //EventManager.PostNotification(GameEvents.PlayerJoined, this, "Client joined");
    }

    public override void Disconnect()
    {
        base.Disconnect();        
    }

    protected override void OnDisconnect(NetworkMessage msg)   
    {
        EventManager.PostNotification(GameEvents.PlayerLeft, this, "LEFT");
    }

    private void OnTeamTypeReceived(NetworkMessage msg)
    {
        TeamTypeMessage msgA = msg.ReadMessage<TeamTypeMessage>();
        EventManager.PostNotification(GameEvents.ReceivedTeamType, this, msgA.TeamType);
        EventManager.PostNotification(GameEvents.PlayerJoined, this, "Client joined");
    }
   
    protected override void AddHandlers()
    {        
        //Custom NETWORK messages
        RegisterHandler(NetworkMessages.MsgChat, OnChatMessageReceived);
        RegisterHandler(NetworkMessages.MsgScore, OnScoreMessageReceived);
        RegisterHandler(NetworkMessages.OpponentCard, OnOpponentCardReceived);
        RegisterHandler(NetworkMessages.MsgTeamType, OnTeamTypeReceived);
        RegisterHandler(NetworkMessages.MsgPlayerLeft, OnDisconnect);   
    }   
}