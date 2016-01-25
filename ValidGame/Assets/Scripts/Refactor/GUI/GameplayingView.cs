using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default gui view for when the player is hovering over the game board.
/// </summary>
public class GameplayingView : View{    
    public void OpenCardView(){
        Debug.Log("Opening");       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            presenter.Restart();
        }
    }
}