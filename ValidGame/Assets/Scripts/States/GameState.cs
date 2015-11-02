using UnityEngine;
using System.Collections;

namespace VALIDGame
{
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
}

