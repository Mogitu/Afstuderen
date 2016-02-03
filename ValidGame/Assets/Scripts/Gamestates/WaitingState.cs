using UnityEngine;
using System.Collections;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .....
/// </summary>
public class WaitingState : GameState {

    public WaitingState(MainManager manager)
            : base(manager){
        manager.eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, OnPlayerJoined);
    }

    public override void UpdateState()
    {
        
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {
        gameManager.StartMultiplayerMatch();
    }
}
