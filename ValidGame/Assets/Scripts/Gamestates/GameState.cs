
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :    Inherit from this class to create new gamestates for use in the gamemanager.
///              Can include basic states such as gameover etc but also new types of gamemodes.
/// TODO    :   I don't like the hardcoded dependency on the Eventmanager, which is a completely unrelated class.
/// </summary>
public abstract class GameState: IGameState
{
    protected EventManager EventManager;
    //overridable base methods
    public GameState(EventManager eventManager)
    {
        EventManager = eventManager;
    }
    public virtual void UpdateState() { }
}