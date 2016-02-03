﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Presenter that is responsible for the main gui and underlying views.
/// TODO    :   When this class grows to big it is an idea to split the gui up with multiple presenters.
/// </summary>
public class GuiPresenter : Presenter
{
    /// <summary>
    ///Desc :   This enum needs to map to existing view names 1 on 1.
    ///TODO :   Enum is probably not the best solution, but for now workable.
    /// </summary>
    public enum VIEWS
    {
        MainmenuView,
        GamePlayingView,
        GameovermenuView,
        CardbrowserView,
        MultiplayermenuView,
        MultiplayerGameplayView
    }

    public MainManager mainManager;
    public EventManager eventManager;

    void Start()
    {
        foreach (View v in views)
        {
            v.SetPresenter(this);
        }
        ChangeView(VIEWS.MainmenuView.ToString());
        eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, StartMatch);
    }

    public void StartPracticeRound()
    {
        ChangeView(VIEWS.GamePlayingView.ToString());
        mainManager.StartPracticeRound();
    }

    public void StartMultiplayerClient(string ip)
    {
        ChangeView(VIEWS.GamePlayingView.ToString());
        OpenView(VIEWS.MultiplayerGameplayView.ToString());
        mainManager.StartMultiplayerClient(ip);
    }

    //TODO  : start host instead of client.
    public void StartMultiplayerHost(string ip)
    {
        ChangeView(VIEWS.MultiplayerGameplayView.ToString());
        mainManager.StartMultiplayerHost(ip);
    }

    public void StartMatch(short event_type, Component sender, object Param=null)
    {
        OpenView(VIEWS.GamePlayingView.ToString());        
    }

    public void Restart()
    {
        mainManager.RestartGame();
    }

    public void QuitApplication()
    {
        mainManager.QuitApplication();
    }

    public void ToggleCardbrowser()
    {
        mainManager.ToggleCameraActive();
        mainManager.ToggleAllColliders();
        ToggleView(VIEWS.CardbrowserView.ToString());      
    }

    public void ShowMainmenuView()
    {
        ChangeView(VIEWS.MainmenuView.ToString());        
    }

    public void EndPracticeGame()
    {
        mainManager.EndPracticeGame();
    }

    public void EndMultiplayerGame()
    {
        mainManager.EndMultiplayerGame();
    }

    //TODO  :   Dont like the way this is coupled to the mainmanager, and routes it to the current gamestate.
    public void PickCard(string code)
    {
        ToggleCardbrowser();
        mainManager.PickCard(code);
    }

    public void ShowGameOverView()
    {
        ChangeView(VIEWS.GameovermenuView.ToString());        
    }

    public void ShowMultiplayerView()
    {
        ChangeView(VIEWS.MultiplayermenuView.ToString());
    }    

    public void PostChatSend(string str)
    {
        eventManager.PostNotification(EVENT_TYPE.SENDCHAT, this, str);       
    }     

    public void PostScoreSend(string str)
    {
        eventManager.PostNotification(EVENT_TYPE.SENDSCORE, this, str);
    }

    /// <summary>
    /// Update a text with the topic description on an object, if existing.
    /// </summary>
    /// <param name="txt">The text mesh to update.</param>
    /// <param name="hit">The hit to use.</param>
    public void UpdateContextInformationText(Text txt, RaycastHit hit)
    {
        ContextDescription desc = hit.transform.gameObject.GetComponentInChildren<ContextDescription>();
        if (desc != null)
        {
            txt.text = desc.descriptionTxt.text;
        }
    }
}