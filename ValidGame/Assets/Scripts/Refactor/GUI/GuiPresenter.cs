using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Concrete presenter that is responsible for the main gui and underlying views.
/// </summary>
public class GuiPresenter : Presenter {    
    
    public MainManager mainManager;
    
    public override void Awake()
    {
        base.Awake();
    }  
    
    public void StartPracticeRound()
    {

        mainManager.StartPracticeRound();
    }       	
   
    public void QuitApplication()
    {
        mainManager.QuitApplication();
    }   
}
