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
    private GuiPresenter Presenter;

    public override void Awake()
    {
        base.Awake();
        Presenter.EventManager.AddListener(GameEvents.PlayerJoined, OnPlayerJoined);
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Player joined");
        PopUpText.text = "Player joined";
    }

    public override void SetPresenter(IPresenter presenter)
    {
        this.Presenter = (GuiPresenter)presenter;
    }
}

