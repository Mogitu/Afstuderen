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
    public GameObject GameOverOverviewParent;
    public GameObject GameOverAnalyzerParent;
    private GuiPresenter GuiPresenter;
    private int OwnScore;
    private int OtherScore;

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

    public void Analyze()
    {
        GuiPresenter.StartAnalyzing();
        GameOverOverviewParent.SetActive(false);
        GameOverAnalyzerParent.SetActive(true);     
    }


    public void StopAnalyzing()
    {
        GameOverOverviewParent.SetActive(true);
        GameOverAnalyzerParent.SetActive(false);
        GuiPresenter.StopAnalyzing();
    }

    public void Restart()
    {
        GuiPresenter.Restart();
    }    

    private void OnScoreReceived(short eventType, Component sender, object param = null)
    {
        Debug.Log("Received in gui score of " + param);
        OwnScore = (int)param;
        OwnScoreTxt.text = "You have " +param + " good cards.";
    }

    private void OnScoreReceivedMP(short eventType, Component sender, object param = null)
    {        
        OtherScoreTxt.text = "Your opponent has " + param + " good cards.";
        OtherScore = (int)param;
        GuiPresenter.EventManager.PostNotification(GameEvents.SendScoreNetwork, this, GuiPresenter.MainManager.Score);
        if(OwnScore > OtherScore)
        {
            gameResultTxt.text = "You Won!";
        }
        else
        {
            gameResultTxt.text = "You Lost!";
        }
    }   
}