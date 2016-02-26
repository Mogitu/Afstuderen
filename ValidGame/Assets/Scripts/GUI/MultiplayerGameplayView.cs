﻿using UnityEngine.UI;
using UnityEngine;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   View that contains all elements required for a multiplayer game.
///             Should be shown together with the default gameplay view.
/// TODO    :   The append methods are very vulnerable to errors in usage and have ambiguous names, refactor!
/// </summary>
public class MultiplayerGameplayView : View {
    public InputField InputField;
    public Text MessageTxt;
    public Text ChatBoxTxt;
    private GuiPresenter Presenter;

    public override void Awake()
    {
        base.Awake();
        Presenter.EventManager.AddListener(GameEvents.ReceiveChatNetwork, OnChatReceived);
        Presenter.EventManager.AddListener(GameEvents.PlayerJoined, OnPlayerJoined);
        Presenter.EventManager.AddListener(GameEvents.PlayerLeft, OnPlayerLeft);
    }

    //TODO  :   More descriptive naming.
    public string AppendTextToBox()
    {       
        string strA = InputField.text+"NEWLINE";
        string strB = strA.Replace("NEWLINE", "\n");
        ChatBoxTxt.text += strB;      
        InputField.text = "";
        return strB;              
    }   
    
    //TODO  :   More descriptive naming.
    public void AppendAndSend()
    {
        string str = AppendTextToBox();
        Presenter.PostChatSend(str);
    }

    //TODO  :   More descriptive naming.
    public void AppendSingle(string str)
    {
        string strA = str + "NEWLINE";
        string strB = strA.Replace("NEWLINE", "\n");
        ChatBoxTxt.text += strB;
        InputField.text = "";
    }

    public void OnChatReceived(short Event_Type, Component Sender, object Param = null)
    {      
        string msg = Param.ToString().Trim();
        AppendSingle(msg);
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {
        AppendSingle("Player joined!");
        MessageTxt.gameObject.SetActive(false);
    }

    public void OnPlayerLeft(short Event_Type, Component Sender, object Param = null)
    {
        AppendSingle("Player left, gameover!");
        Presenter.EndMultiplayerGame();
    }

    public override void SetPresenter(IPresenter presenter)
    {
        this.Presenter = (GuiPresenter)presenter;
    }
}
