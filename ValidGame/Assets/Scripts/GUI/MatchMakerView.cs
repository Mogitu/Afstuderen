using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   View that is shown when a connection is being made to the server and/or the player is being matched with an opponent.
/// </summary>
public class MatchMakerView : View {
    public Text ConnectionStatusText;
    public Text MatchStatusText;
      
	// Use this for initialization
	void Start () {
        ((GuiPresenter)Presenter).EventManager.AddListener(GameEvents.SuccesfullConnection, OnConnection);
        ((GuiPresenter)Presenter).EventManager.AddListener(GameEvents.PlayerJoined, OnPlayerJoined);
    }

    private void OnConnection(short eventType, Component sender, object param=null)
    {
        ConnectionStatusText.color = Color.green;
        ConnectionStatusText.text = "Connected to server....";
        MatchStatusText.gameObject.SetActive(true);
    }

    private void OnPlayerJoined(short eventType, Component sender, object param = null)
    {
       
        MatchStatusText.color = Color.green;
        MatchStatusText.text = "Match found!!!";
    }
}