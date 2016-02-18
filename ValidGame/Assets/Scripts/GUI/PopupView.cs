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
    public Text popUpText;
    private GuiPresenter presenter;

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

    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }
}

