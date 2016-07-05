using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Display feedback messages on screen
/// TODO    :   This class should get listeners for all messages.
///             A fadein and fadeout coroutine would be "nice".
/// </summary>
public class PopupView : View
{
    public Text PopUpText;
    private GuiPresenter GuiPresenter;

    void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        GuiPresenter.EventManager.AddListener(GameEvents.PlayerJoined, OnPlayerJoined);
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Player joined");
        PopUpText.text = "Player joined";
    }
}

