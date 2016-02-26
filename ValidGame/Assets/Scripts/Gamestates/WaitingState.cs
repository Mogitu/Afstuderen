using UnityEngine;
using System.Collections;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .....
/// </summary>
public class WaitingState : GameState {

    public WaitingState(MainManager manager)
            : base(manager){
        manager.EventManager.AddListener(GameEvents.PlayerJoined, OnPlayerJoined);
    }

    public override void UpdateState()
    {
        
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {
        GameManager.StartMultiplayerMatch();
    }
}
