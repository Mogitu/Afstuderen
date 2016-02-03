using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   View that contains all elements required for a multiplayer game.
///             Should be shown together with the default gameplay view.
/// TODO    :   The append methods are very vulnerable to errors in usage, refactor?
/// </summary>
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

    //TODO  :   More descriptive naming.
    public string AppendTextToBox()
    {       
        string strA = inputField.text+"NEWLINE";
        string strB = strA.Replace("NEWLINE", "\n");
        chatBoxTxt.text += strB;      
        inputField.text = "";
        return strB;              
    }   
    
    //TODO  :   More descriptive naming.
    public void AppendAndSend()
    {
        string str = AppendTextToBox();
        presenter.PostChatSend(str);
    }

    //TODO  :   More descriptive naming.
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
