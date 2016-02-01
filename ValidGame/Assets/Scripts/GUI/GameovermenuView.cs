using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Displays game over items, like scores etc.
/// </summary>
public class GameovermenuView : View {

    public Text ownScoreTxt;
    public Text otherScoreTxt;  

    public override void Awake()
    {
        base.Awake();
        presenter.eventManager.AddListener(EVENT_TYPE.SENDSCORE, OnScoreReceived);
        presenter.eventManager.AddListener(EVENT_TYPE.RECEIVESCOREMP, OnScoreReceivedMP);
        if(presenter.mainManager.IsMultiplayerGame)
        {
            otherScoreTxt.text = "Waiting for opponent to finish.";
        }
        else
        {
            otherScoreTxt.text = "";
        }        
    }

    public void Restart()
    {
        presenter.Restart();
    }    

    public void OnScoreReceived(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Received score of " + Param);
        ownScoreTxt.text = "You have " +Param + " good cards.";
    }

    public void OnScoreReceivedMP(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {        
        otherScoreTxt.text = "Your opponent has " + Param + " good cards.";
        presenter.eventManager.PostNotification(EVENT_TYPE.SENDSCOREMP, this, presenter.mainManager.score);
    }
}
