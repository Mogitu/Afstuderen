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
    public int SocketPort = 7777;

    void Start()
    {
        EventManager.AddListener(GameEvents.SendSchat, SendChatMsgs);
        EventManager.AddListener(GameEvents.SendScoreNetwork, SendScoreMsgs);
        EventManager.AddListener(GameEvents.SendCardToOpponent, SendOpponentCard);
    }

    public void SendOpponentCard(short Event_Type, Component Sender, object param = null)
    {
        CardToOpponentMessage msgA = (CardToOpponentMessage)param;
        SendMessage(NetworkMessages.OpponentCard, msgA);
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
        CreateServerContext<AmcServer>(SocketPort);
        AddHandlers();
        IsClient = false;
    }

    public override void StartClient(string ip)
    {
        CreateClientContext<AmcClient>(ip, SocketPort);
        AddHandlers();
        IsClient = true;
    }

    private void SendChatMsgs(short event_Type, Component sender, object param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.Text = param.ToString();
        SendMessage(NetworkMessages.MsgChat, msgA);
    }

    private void SendScoreMsgs(short event_Type, Component sender, object param = null)
    {
        ScoreMessage msgA = new ScoreMessage();
        msgA.Score = (int)param;
        SendMessage(NetworkMessages.MsgScore, msgA);
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

    private void OnPlayerConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(GameEvents.PlayerJoined, this, "Client joined");
    }

    private void OnPlayerDissConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(GameEvents.PlayerLeft, this, "LEFT");
    }

    protected override void AddHandlers()
    {
        //Build in NETWORK messages
        RegisterHandler(MsgType.Connect, OnPlayerConnect);
        RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);

        //Custom NETWORK messages
        RegisterHandler(NetworkMessages.MsgChat, OnChatMessageReceived);
        RegisterHandler(NetworkMessages.MsgScore, OnScoreMessageReceived);
        RegisterHandler(NetworkMessages.OpponentCard, OnOpponentCardReceived);
    }
}