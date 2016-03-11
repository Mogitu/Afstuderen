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
    public Text gameResultTxt;
    private GuiPresenter GuiPresenter;
    private int ownScore;
    private int otherScore;

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

    public void OnScoreReceived(short eventType, Component sender, object param = null)
    {
        Debug.Log("Received in gui score of " + param);
        ownScore = (int)param;
        OwnScoreTxt.text = "You have " +param + " good cards.";
    }

    public void OnScoreReceivedMP(short eventType, Component sender, object param = null)
    {        
        OtherScoreTxt.text = "Your opponent has " + param + " good cards.";
        otherScore = (int)param;
        GuiPresenter.EventManager.PostNotification(GameEvents.SendScoreNetwork, this, GuiPresenter.MainManager.Score);
        if(ownScore > otherScore)
        {
            gameResultTxt.text = "You Won!";
        }
        else
        {
            gameResultTxt.text = "You Lost!";
        }
    }   
}