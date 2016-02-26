/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls the flow of available gamestates in relation to the manager.
/// TODO    :   Depends on concrete Mainmanager, should rather depend on an manager abstraction.(SOLID)
/// </summary>
public class GameStateManager {

    private GameState GameState;
    private PlayingState PlayingState;
    private WaitingState WaitingState;
    private MultiplayerState MultiPlayerState;
    private GameOverState GameoverState;

    public GameStateManager(MainManager manager)
    {
        PlayingState = new PlayingState(manager);
        WaitingState = new WaitingState(manager);
        MultiPlayerState = new MultiplayerState(manager);
        GameoverState = new GameOverState(manager);
        GameState = WaitingState;
    }    
	
	public void UpdateCurrentState()
    {
        GameState.UpdateState();
    }

    public void SetPlayingState()
    {
        GameState = PlayingState;
    }

    public void SetWaitingState()
    {
        GameState = WaitingState;
    }

    public void SetGameoverState()
    {
        GameState = GameoverState;
    }

    public void SetMultiplayerState()
    {
        GameState = MultiPlayerState;
    }
}
