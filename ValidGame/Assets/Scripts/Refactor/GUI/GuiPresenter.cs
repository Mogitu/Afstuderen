using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Presenter that is responsible for the main gui and underlying views.
/// </summary>
public class GuiPresenter : Presenter {    

    public enum VIEWS
    {
        MainmenuView,
        GamePlayingView,
        GameoverView,
        CardbrowserView,
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
        Debug.Log("starting multiplayer");
    }

    public void Restart()
    {
        Debug.Log("Restarting");
        //ChangeView(VIEWS.MainmenuView.ToString());
    }
   
    public void QuitApplication()
    {
        mainManager.QuitApplication();
    }     

    public void ToggleCardbrowser()
    {
        ToggleView(VIEWS.CardbrowserView.ToString());  
    }  

    /// <summary>
    /// Update a text with the topic description on an object, if existing.
    /// </summary>
    /// <param name="txt">The text mesh to update.</param>
    /// <param name="hit">The hit to use.</param>
    public void UpdateGuiText(Text txt, RaycastHit hit)
    {
        SubtopicDescription desc = hit.transform.gameObject.GetComponentInChildren<SubtopicDescription>();
        if(desc !=null)
        {
            txt.text = desc.descriptionTxt.text;
        }        
    }
}
