using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Main menu. Start, quit, etc.
/// </summary>
public class MainmenuView : View {       

    public void ClickedStart()
    {          
        presenter.StartPracticeRound();            
    }

    public void ClickedMultiplayer()
    {
        presenter.StartMultiplayerRound();     
    }

    public void ClickedQuit()
    {
        presenter.QuitApplication();
    }
}