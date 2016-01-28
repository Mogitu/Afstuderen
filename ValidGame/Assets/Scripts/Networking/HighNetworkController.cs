using UnityEngine;
using UnityEngine.Networking;

public class MsgTypes
{
    public static short MSG_CHAT = MsgType.Highest + 1;
}

public class ChatMessage : MessageBase
{
    public string text;
}


public class HighNetworkController : NetworkManager
{
    public EventManager eventManager;
    private NetworkClient myClient;

    void Start()
    {
        eventManager.AddListener(EVENT_TYPE.SEND, SendMsgs);
    }

    public void SendMsgs(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        ChatMessage msgA = new ChatMessage();
        msgA.text = Param.ToString();
        if (myClient != null)
        {
            myClient.Send(MsgTypes.MSG_CHAT, msgA);
        }
        NetworkServer.SendToAll(MsgTypes.MSG_CHAT, msgA);
        Debug.Log("Sent " + msgA.text);
    }

    public void StartClient(string ip)
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        myClient.Connect(ip, 7777);
    }

    void OnChatMessageReceived(NetworkMessage msg)
    {
        ChatMessage msgA = msg.ReadMessage<ChatMessage>();
        Debug.Log("JAAAAA");
        eventManager.PostNotification(EVENT_TYPE.RECEIVE, this, msgA.text.ToString());
    }

    public void MyStartHost()
    {
        NetworkServer.Listen(7777);
        // myClient = ClientScene.ConnectLocalServer();
        //StartHost();
        NetworkServer.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        // myClient.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        // StartServer();
        // myClient = new NetworkClient();        
        //myClient.RegisterHandler(MsgTypes.MSG_CHAT, OnChatMessageReceived);
        // myClient.Connect("127.0.0.1", 7777);                    
    }
}

