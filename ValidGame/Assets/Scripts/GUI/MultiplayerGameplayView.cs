using UnityEngine.UI;
using UnityEngine;

public class MultiplayerGameplayView : View {
    public InputField inputField;
    public Text messageTxt;
    public Text chatBoxTxt;

    public override void Awake()
    {
        base.Awake();
        presenter.eventManager.AddListener(EVENT_TYPE.RECEIVECHATNETWORK, OnChatReceived);
        presenter.eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, OnPlayerJoined);
        presenter.eventManager.AddListener(EVENT_TYPE.PLAYERLEFT, OnPlayerLeft);
    }

    public string AppendTextToBox()
    {       
        string strA = inputField.text+"NEWLINE";
        string strB = strA.Replace("NEWLINE", "\n");
        chatBoxTxt.text += strB;      
        inputField.text = "";
        return strB;              
    }   
    
    public void AppendAndSend()
    {
        string str = AppendTextToBox();
        presenter.PostChatSend(str);
    }

    public void AppendSingle(string str)
    {
        string strA = str + "NEWLINE";
        string strB = strA.Replace("NEWLINE", "\n");
        chatBoxTxt.text += strB;
        inputField.text = "";
    }

    public void OnChatReceived(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {      
        string msg = Param.ToString().Trim();
        AppendSingle(msg);
    }

    public void OnPlayerJoined(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        AppendSingle("Player joined!");
        messageTxt.gameObject.SetActive(false);
    }

    public void OnPlayerLeft(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        AppendSingle("Player left, gameover!");
        presenter.EndMultiplayerGame();
    }
}
