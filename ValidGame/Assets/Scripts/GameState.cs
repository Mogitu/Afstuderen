using UnityEngine;
using System.Collections;

public abstract class GameState {    

    //overridable base methods
    public GameState() { }    
    public virtual void UpdateState(GameObject gameObject){}
}
