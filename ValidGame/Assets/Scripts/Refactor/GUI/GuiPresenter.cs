using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Presenter that is responsible for the main gui and underlying views.
/// </summary>
public class GuiPresenter : Presenter {    

    public enum VIEWS
    {
        MainmenuView,
        GamePlayingView,
        GameoverView
    }
    
    public MainManager mainManager;   
    
    void Awake()
    {
        ChangeView(VIEWS.MainmenuView.ToString());
    }   
    
    public void StartPracticeRound()
    {
        ChangeView(VIEWS.GamePlayingView.ToString());
        mainManager.StartPracticeRound();
    }       	

    public void StartMultiplayerRound()
    {

    }

    public void Restart()
    {
        Debug.Log("Restarting");
        ChangeView(VIEWS.MainmenuView.ToString());
    }
   
    public void QuitApplication()
    {
        mainManager.QuitApplication();
    }     
}
