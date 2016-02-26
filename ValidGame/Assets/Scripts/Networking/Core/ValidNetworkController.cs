using UnityEngine;
using UnityEngine.Networking;
using System;
using AMC.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Highlevel networkmanager
/// </summary>

public class ValidNetworkController : NetworkController
{
    public EventManager EventManager;
    public int SocketPort = 7777;

    private AmcClient Client;
    private AmcServer Server;
    private bool IsClient { get; set; }

    void Start()
    {
        EventManager.AddListener(GameEvents.SendSchat, SendChatMsgs);
        EventManager.AddListener(GameEvents.SendScoreNetwork, SendScoreMsgs);
        EventManager.AddListener(GameEvents.SendCardToOpponent, SendOpponentCard);
    }

    public void SendOpponentCard(short Event_Type, Component Sender, object param = null)
    {
        Debug.Log("Sending card");
        CardToOpponentMessage msgA = (CardToOpponentMessage)param;
        if (IsClient && Client != null)
        {
            Client.SendMessage(NetworkMessages.OpponentCard, msgA);
        }
        else
        {
            Server.SendMessage(NetworkMessages.OpponentCard, msgA);
        }
    }

    public void OnOpponentCardReceived(NetworkMessage msg)
    {
        CardToOpponentMessage msgA = msg.ReadMessage<CardToOpponentMessage>();
        Debug.Log(msgA.CardName);
        string name = msgA.CardName.Trim();
        GameObject go = Instantiate(GameObject.Find(name).gameObject);
        go.transform.position = msgA.Position;
    }

    public override void BeginHosting()
    {
        Server = new AmcServer(SocketPort);
        Server.RegisterHandler(NetworkMessages.MsgChat, OnChatMessageReceived);
        Server.RegisterHandler(NetworkMessages.MsgScore, OnScoreMessageReceived);
        Server.RegisterHandler(MsgType.Connect, OnPlayerConnect);
        Server.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        Server.RegisterHandler(NetworkMessages.OpponentCard, OnOpponentCardReceived);
    }

    public override void StartClient(string ip)
    {
        Client = new AmcClient(ip, SocketPort);
        Client.RegisterHandler(NetworkMessages.MsgChat, OnChatMessageReceived);
        Client.RegisterHandler(NetworkMessages.MsgScore, OnScoreMessageReceived);
        Client.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        Client.RegisterHandler(NetworkMessages.OpponentCard, OnOpponentCardReceived);
        IsClient = true;
    }

    public void SendChatMsgs(short event_Type, Component sender, object param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.Text = param.ToString();
        if (IsClient && Client != null)
        {
            Client.SendMessage(NetworkMessages.MsgChat, msgA);
        }
        else
        {
            Server.SendMessage(NetworkMessages.MsgChat, msgA);
        }
    }

    public void SendScoreMsgs(short event_Type, Component sender, object param = null)
    {
        ScoreMessage msgA = new ScoreMessage();
        msgA.Score = (int)param;
        Debug.Log("sending as score to gui: " + msgA.Score);
        if (IsClient && Client != null)
        {
            Client.SendMessage(NetworkMessages.MsgScore, msgA);
        }
        else
        {
            Server.SendMessage(NetworkMessages.MsgScore, msgA);
        }
    }

    void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();
        Debug.Log("Received Chat");
        EventManager.PostNotification(GameEvents.ReceiveChatNetwork, this, msgA.Text.ToString());
    }

    void OnScoreMessageReceived(NetworkMessage msg)
    {
        ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();
        Debug.Log("Received network score " + msgA.Score);
        EventManager.PostNotification(GameEvents.ReceiveScoreNetwork, this, msgA.Score);
    }

    void OnPlayerConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(GameEvents.PlayerJoined, this, "JOINED");
        if (Server.ConnectionCount >= 2)
        {
            Debug.Log("Game is ready");
        }
    }

    void OnPlayerDissConnect(NetworkMessage msg)
    {
        EventManager.PostNotification(GameEvents.PlayerLeft, this, "LEFT");
    }

    public override void Disconnect()
    {
        // throw new NotImplementedException();
    }

    public override void ReceiveMessage()
    {
        //throw new NotImplementedException();
    }

    public override void SendMessage()
    {
        // throw new NotImplementedException();
    }

    public override void AddListeners()
    {
        throw new NotImplementedException();
    }

    public int GetConnectionsCount
    {
        get { return NetworkServer.connections.Count; }
    }
}

