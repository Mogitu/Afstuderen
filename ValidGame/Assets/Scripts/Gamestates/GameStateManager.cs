using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls the flow of available gamestates in relation to the manager.
/// TODO    :   Depends on concrete Mainmanager, should rather depend on an manager abstraction.(SOLID)
/// </summary>
public class GameStateManager : MonoBehaviour{    
    public EventManager EventManager;
    private GameState _GameState;
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
        _GameState = WaitingState;
        AddListeners();
    }

    public GameState GameState { get { return _GameState; } }

    private void AddListeners()
    {
        EventManager.AddListener(GameEvents.BeginPractice, SetPlayingState);
        EventManager.AddListener(GameEvents.BeginMultiplayer, SetMultiplayerState);
        EventManager.AddListener(GameEvents.EndPractice, SetGameoverState);
    }
    
    private void Update()
    {
        _GameState.UpdateState();
    }	

    public void SetPlayingState(short gameEvent, Component sender, object obj)
    {
        _GameState = PlayingState;
    }

    public void SetWaitingState()
    {
        _GameState = WaitingState;
    }

    public void SetGameoverState(short gameEvent, Component sender, object obj)
    {
        _GameState = GameoverState;
    }

    public void SetMultiplayerState(short gameEvent, Component sender, object obj)
    {
        _GameState = MultiPlayerState;
    }

    public GameState CurrentState
    {
        get { return _GameState; }
    }
}
