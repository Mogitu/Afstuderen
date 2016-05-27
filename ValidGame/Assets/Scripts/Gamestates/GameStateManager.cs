using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls the flow of available gamestates in relation to the manager.
/// TODO    :   Depends on concrete Mainmanager, should rather depend on an manager abstraction.(SOLID)
/// </summary>
public class GameStateManager : MonoBehaviour{    
    public EventManager EventManager;
    private GameState GameState;
    private PlayingState PlayingState;
    private WaitingState WaitingState;
    private MultiplayerState MultiPlayerState;
    private GameOverState GameoverState;

    void Start()
    {
        PlayingState = new PlayingState(EventManager);
        WaitingState = new WaitingState(EventManager);
        MultiPlayerState = new MultiplayerState(EventManager);
        GameoverState = new GameOverState(EventManager);
        GameState = WaitingState;
        AddListeners();
    }

    private void AddListeners()
    {
        EventManager.AddListener(GameEvents.BeginPractice, SetPlayingState);
        EventManager.AddListener(GameEvents.BeginMultiplayer, SetMultiplayerState);
        EventManager.AddListener(GameEvents.EndPractice, SetGameoverState);
    }
    
    private void Update()
    {
        GameState.UpdateState();
    }	

    public void SetPlayingState(short gameEvent, Component sender, object obj)
    {
        GameState = PlayingState;
    }

    public void SetWaitingState()
    {
        GameState = WaitingState;
    }

    public void SetGameoverState(short gameEvent, Component sender, object obj)
    {
        GameState = GameoverState;
    }

    public void SetMultiplayerState(short gameEvent, Component sender, object obj)
    {
        GameState = MultiPlayerState;
    }

    public GameState CurrentState
    {
        get { return GameState; }
    }
}
