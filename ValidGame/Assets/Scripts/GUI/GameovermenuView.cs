using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Displays game over items, like scores etc.
/// </summary>
public class GameovermenuView : View {

    public Text OwnScoreTxt;
    public Text OtherScoreTxt;
    private GuiPresenter Presenter;

    public override void Awake()
    {
        base.Awake();
        Presenter.EventManager.AddListener(GameEvents.SendScore, OnScoreReceived);
        Presenter.EventManager.AddListener(GameEvents.ReceiveScoreNetwork, OnScoreReceivedMP);
       
        if(Presenter.MainManager.IsMultiplayerGame)
        {
            OtherScoreTxt.text = "Waiting for opponent to finish.";
        }
        else
        {
            OtherScoreTxt.text = "";
        }             
    }

    public void Restart()
    {
        Presenter.Restart();
    }    

    public void OnScoreReceived(short Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Received in gui score of " + Param);
        OwnScoreTxt.text = "You have " +Param + " good cards.";
    }

    public void OnScoreReceivedMP(short Event_Type, Component Sender, object Param = null)
    {        
        OtherScoreTxt.text = "Your opponent has " + Param + " good cards.";
        Presenter.EventManager.PostNotification(GameEvents.SendScoreNetwork, this, Presenter.MainManager.Score);
    }

    public override void SetPresenter(IPresenter presenter)
    {
        Presenter = (GuiPresenter)presenter;
    }
}
