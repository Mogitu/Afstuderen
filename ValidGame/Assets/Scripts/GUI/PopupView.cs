using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class PopupView : View
{
    public Text popUpText;

    public override void Awake()
    {
        base.Awake();
        presenter.eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, OnPlayerJoined);

    }

    public void OnPlayerJoined(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Player joined");
        popUpText.text = "Player joined";
    }
}

