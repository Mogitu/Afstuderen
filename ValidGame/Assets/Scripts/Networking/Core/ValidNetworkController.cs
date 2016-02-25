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
        public EventManager eventManager;
        public int socketPort = 7777;

        private NetworkClient myClient;
        private bool isClient;

        void Start()
        {
            eventManager.AddListener(EVENT_TYPE.SENDCHAT, SendChatMsgs);
            eventManager.AddListener(EVENT_TYPE.SENDSCORENETWORK, SendScoreMsgs);
        }

        public override void BeginHosting()
        {
            NetworkServer.Listen(socketPort);
            NetworkServer.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
            NetworkServer.RegisterHandler(MsgTypes.MSG_SCORE, OnScoreMessageReceived);
            NetworkServer.RegisterHandler(MsgType.Connect, OnPlayerConnect);
            NetworkServer.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
        }

        public override void StartClient(string ip)
        {
            myClient = new NetworkClient();
            myClient.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
            myClient.RegisterHandler(MsgTypes.MSG_SCORE, OnScoreMessageReceived);
            myClient.RegisterHandler(MsgType.Disconnect, OnPlayerDissConnect);
            myClient.Connect(ip, socketPort);
            isClient = true;
        }

        public void SendChatMsgs(short Event_Type, Component Sender, object Param = null)
        {
            ChatMessage msgA = new ChatMessage();
            msgA.text = Param.ToString();
            if (isClient && myClient != null)
            {
                myClient.Send(MsgTypes.MSG_CHAT, msgA);
            }
            else
            {
                NetworkServer.SendToAll(MsgTypes.MSG_CHAT, msgA);
            }
        }

        public void SendScoreMsgs(short Event_Type, Component Sender, object Param = null)
        {
            ScoreMessage msgA = new ScoreMessage();
            msgA.score = (int)Param;
            Debug.Log("sending as score to gui: " + msgA.score);
            if (isClient && myClient != null)
            {
                myClient.Send(MsgTypes.MSG_SCORE, msgA);
            }
            else
            {
                NetworkServer.SendToAll(MsgTypes.MSG_SCORE, msgA);
            }
        }

        void OnChatMessageReceived(NetworkMessage msg)
        {
            ChatMessage msgA = msg.ReadMessage<ChatMessage>();
            Debug.Log("Received Chat");
            eventManager.PostNotification(EVENT_TYPE.RECEIVECHATNETWORK, this, msgA.text.ToString());
        }

        void OnScoreMessageReceived(NetworkMessage msg)
        {
            ScoreMessage msgA = msg.ReadMessage<ScoreMessage>();
            Debug.Log("Received network score " + msgA.score);
            eventManager.PostNotification(EVENT_TYPE.RECEIVESCORENETWORK, this, msgA.score);
        }

        void OnPlayerConnect(NetworkMessage msg)
        {
            eventManager.PostNotification(EVENT_TYPE.PLAYERJOINED, this, "JOINED");
            if (NetworkServer.connections.Count >= 2)
            {
                Debug.Log("Game is ready");
            }
        }

        void OnPlayerDissConnect(NetworkMessage msg)
        {
            eventManager.PostNotification(EVENT_TYPE.PLAYERLEFT, this, "LEFT");
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

