using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Main menu. Start, quit, etc.
/// </summary>
public class MainmenuView : View {       

    public void ClickedStart()
    {          
        presenter.StartPracticeRound();        
        Debug.Log("start");
    }

    public void ClickedMultiplayer()
    {
        presenter.StartMultiplayerRound();
        Debug.Log("Multiplayer");
    }

    public void ClickedQuit()
    {
        presenter.QuitApplication();
    }
}