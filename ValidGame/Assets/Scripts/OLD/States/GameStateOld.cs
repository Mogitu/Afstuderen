using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :    Inherit from this class to create new gamestates for use in the gamemanager.
///              Can include basic states such as gameover etc but also new types of gamemodes.
/// </summary>
public abstract class GameStateOld
{
    protected GameManager gameManager;
    //overridable base methods
    public GameStateOld(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public virtual void UpdateState() { }
}