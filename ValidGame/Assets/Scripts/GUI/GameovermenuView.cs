using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Displays game over items, like scores etc.
/// </summary>
public class GameovermenuView : View {

    public Text ownScoreTxt;
    public Text otherScoreTxt;
    private GuiPresenter presenter;

    public override void Awake()
    {
        base.Awake();
        presenter.eventManager.AddListener(EVENT_TYPE.SENDSCORE, OnScoreReceived);
        presenter.eventManager.AddListener(EVENT_TYPE.RECEIVESCORENETWORK, OnScoreReceivedMP);
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

    public void OnScoreReceived(short Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Received in gui score of " + Param);
        ownScoreTxt.text = "You have " +Param + " good cards.";
    }

    public void OnScoreReceivedMP(short Event_Type, Component Sender, object Param = null)
    {        
        otherScoreTxt.text = "Your opponent has " + Param + " good cards.";
        presenter.eventManager.PostNotification(EVENT_TYPE.SENDSCORENETWORK, this, presenter.mainManager.score);
    }

    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }
}
