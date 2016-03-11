
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :    Inherit from this class to create new gamestates for use in the gamemanager.
///              Can include basic states such as gameover etc but also new types of gamemodes.
/// </summary>
public abstract class GameState: IGameState
{
    protected MainManager GameManager;
    //overridable base methods
    public GameState(MainManager gameManager)
    {
        GameManager = gameManager;
    }
    public virtual void UpdateState() { }
}