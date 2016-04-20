using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls the flow of available gamestates in relation to the manager.
/// TODO    :   Depends on concrete Mainmanager, should rather depend on an manager abstraction.(SOLID), or none at all by using events more efficiently.
/// </summary>
public class GameStateManager:MonoBehaviour {
    
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
        AddListeners();
        GameState = WaitingState;
    }

    private void AddListeners()
    {
        EventManager.AddListener(GameEvents.BeginPracticeMatch,SetPlayingState);
        EventManager.AddListener(GameEvents.EndPracticeGame,SetGameoverState);
        EventManager.AddListener(GameEvents.StartMultiplayerMatch,SetMultiplayerState);
    }
    
    void Update()
    {
        GameState.UpdateState();
    }  

    public void SetPlayingState(short gameEvent, Component sender, object obj)
    {        
        GameState = PlayingState;
    }   

    public void SetGameoverState(short gameEvent, Component sender, object obj)
    {
        GameState = GameoverState;
    }

    public void SetMultiplayerState(short gameEvent, Component sender, object obj)
    {
        GameState = MultiPlayerState;
    }
}
