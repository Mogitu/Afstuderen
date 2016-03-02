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
    private GuiPresenter GuiPresenter;

    public void Awake()
    {        
        GuiPresenter = GetPresenterType<GuiPresenter>();
        GuiPresenter.EventManager.AddListener(GameEvents.SendScore, OnScoreReceived);
        GuiPresenter.EventManager.AddListener(GameEvents.ReceiveScoreNetwork, OnScoreReceivedMP);
       
        if(GuiPresenter.MainManager.IsMultiplayerGame)
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
        GuiPresenter.Restart();
    }    

    public void OnScoreReceived(short Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Received in gui score of " + Param);
        OwnScoreTxt.text = "You have " +Param + " good cards.";
    }

    public void OnScoreReceivedMP(short Event_Type, Component Sender, object Param = null)
    {        
        OtherScoreTxt.text = "Your opponent has " + Param + " good cards.";
        GuiPresenter.EventManager.PostNotification(GameEvents.SendScoreNetwork, this, GuiPresenter.MainManager.Score);
    }   
}
