using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .....
/// </summary>
public class WaitingState : GameState {
    private float Timer = 0.0f;
    private bool RunTimer = false;
    private int MaxTime = 3;

    public WaitingState(MainManager manager)
            : base(manager){
        manager.EventManager.AddListener(GameEvents.PlayerJoined, OnPlayerJoined);
    }

    public override void UpdateState()
    {
        if(RunTimer)
        {
            Timer += Time.deltaTime;
            if(Timer >=MaxTime)
            {
                GameManager.StartMultiplayerMatch();
                Timer = 0;
                RunTimer = false;
                GameManager.EventManager.PostNotification(GameEvents.StartMultiplayerMatch,null);
            }
        }
    }

    public void OnPlayerJoined(short Event_Type, Component Sender, object Param = null)
    {      
        RunTimer = true;
    }
}
