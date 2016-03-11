using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   View that is shown when a connection is being made to the server and/or the player is being matched with an opponent.
/// </summary>
public class MatchMakerView : View {
    public Text connectionStatusText;  
      
	// Use this for initialization
	void Start () {
        ((GuiPresenter)Presenter).EventManager.AddListener(GameEvents.SuccesfullConnection, OnConnection);
	}

    private void OnConnection(short eventType, Component sender, object param=null)
    {
        connectionStatusText.color = Color.green;
        connectionStatusText.text = "Connected to server....";
    }	
}