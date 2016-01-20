using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default gui view for when the player is hovering over the game board.
/// </summary>
public class GameplayingView : MonoBehaviour, IView {
    private GuiPresenter presenter;
    public void SetPresenter(Presenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }


    public void OpenCardView(){
        Debug.Log("Opening");
    }
}
