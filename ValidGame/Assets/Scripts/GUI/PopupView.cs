﻿using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Display feedback messages on screen
/// TODO    :   This class should get listeners for all messages.
///             A fadein and fadeout coroutine would be "nice".
/// </summary>
public class PopupView : View
{
    public Text popUpText;

    public override void Awake()
    {
        base.Awake();
        presenter.eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, OnPlayerJoined);
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Player joined");
        popUpText.text = "Player joined";
    }
}

