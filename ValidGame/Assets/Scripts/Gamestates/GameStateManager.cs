/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls the flow of available gamestates in relation to the manager.
/// TODO    :   Depends on concrete Mainmanager, should rather depend on an manager abstraction.(SOLID)
/// </summary>
public class GameStateManager {

    private GameState gameState;
    private PlayingState playingState;
    private WaitingState waitingState;
    private MultiplayerState multiPlayerState;
    private GameOverState gameoverState;

    public GameStateManager(MainManager manager)
    {
        playingState = new PlayingState(manager);
        waitingState = new WaitingState(manager);
        multiPlayerState = new MultiplayerState(manager);
        gameoverState = new GameOverState(manager);
        gameState = waitingState;
    }    
	
	public void UpdateCurrentState()
    {
        gameState.UpdateState();
    }

    public void SetPlayingState()
    {
        gameState = playingState;
    }

    public void SetWaitingState()
    {
        gameState = waitingState;
    }

    public void SetGameoverState()
    {
        gameState = gameoverState;
    }

    public void SetMultiplayerState()
    {
        gameState = multiPlayerState;
    }
}
