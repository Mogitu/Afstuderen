using UnityEngine;
using System.Collections;


    //----------------------------------------------------------------------------------
    // Class    : GameState
    // Desc     : Inherit from this class to create new gamestates for use in the gamemanager.
    //            Can include basic states such as gameover etc but also new types of gamemodes.
    // ----------------------------------------------------------------------------------
    public abstract class GameState
    {
        protected GameManager gameManager;
        //overridable base methods
        public GameState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        public virtual void UpdateState() { }
    }


