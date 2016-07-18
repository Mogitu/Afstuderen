using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Displays game over items, like scores etc.
/// </summary>
public class GameovermenuView : View
{
    public Text OwnScoreTxt;
    public Text OtherScoreTxt;
    public Text gameResultTxt;
    public GameObject GameOverOverviewParent;
    public GameObject GameOverAnalyzerParent;
    private GuiPresenter GuiPresenter;
    private int OwnScore;
    private int OtherScore;
    private int CardCount;

    public void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        GuiPresenter.EventManager.AddListener(GameEvents.SendScore, OnScoreReceived);
        GuiPresenter.EventManager.AddListener(GameEvents.ReceiveScoreNetwork, OnScoreReceivedMP);
        GuiPresenter.EventManager.AddListener(GameEvents.CardCount, OnCardCountReceived);


        if (GuiPresenter.MainManager.IsMultiplayerGame)
        {
            OtherScoreTxt.text = "Waiting for opponent to finish.";
        }
        else
        {
            OtherScoreTxt.text = "";
        }
        GuiPresenter.EventManager.PostNotification(GameEvents.RequestCardCount, this);
    }

    private void OnCardCountReceived(short eventType, Component sender, object param)
    {
        CardCount = (int)param;
    }

    public void StartAnalyzing()
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
        OwnScore = (int)param;
        double grade = Math.Round(((double)OwnScore / CardCount) * 100, 2);
        //Debug.Log(GuiPresenter.MainManager.TimeUsed);
        double t = (int)param;
        double averageScore = Math.Round(t / (GuiPresenter.MainManager.TimeUsed/60),2);

        var averageScoreText = "Average score of " + averageScore + " cards per minute";
        if (GuiPresenter.GameTimeChanged)
        {
            averageScoreText = "";
        }
        OwnScoreTxt.text = "You placed " + param + " card(s) correct\n" +
            "You scored " + grade + "%" + "\n" + averageScoreText;
            
    }

    private void OnScoreReceivedMP(short eventType, Component sender, object param = null)
    {
        OtherScoreTxt.text = "Your opponent has " + param + " correct cards.";
        OtherScore = (int)param;
        GuiPresenter.EventManager.PostNotification(GameEvents.SendScoreNetwork, this, GuiPresenter.MainManager.Score);
        if (OwnScore > OtherScore)
        {
            gameResultTxt.text = "You Won!";
        }
        else
        {
            gameResultTxt.text = "You Lost!";
        }
    }
}