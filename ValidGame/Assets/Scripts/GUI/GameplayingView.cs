using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default gui view for when the player is hovering over the game board.
/// </summary>
public class GameplayingView : View{
    public GameObject FinishButton;
    public GameObject InfoBar;
    public Text InfoText;
    private GuiPresenter GuiPresenter;

    void Awake()
    {
        FinishButton.SetActive(false);
        GuiPresenter = GetPresenterType<GuiPresenter>();
        GuiPresenter.EventManager.AddListener(GameEvents.GameIsFinishable,OnGameIsFinishable);
        GuiPresenter.EventManager.AddListener(GameEvents.UndoGameFinishable, OnUndoGameFinishable);
    }    

    public void OpenCardView(){       
        GuiPresenter.ToggleCardbrowser();    
    }

    public void ToggleOptionsView()
    {
        GuiPresenter.ToggleOptionsView();
    }

    public void ToggleTutorial()
    {
        GuiPresenter.ToggleTutorial();
    }

    void FixedUpdate()
    {
        CheckForDescriptionHit();
    }

    void Update()
    {        
        //CheckForDescriptionHit();
    }

    public void Restart()
    {
        GuiPresenter.Restart();
    }

    public void OnGameIsFinishable(short gameEvent, Component sender, object param=null)
    {
        FinishButton.SetActive(true);
    }

    public void OnUndoGameFinishable(short gameEvent, Component sender, object param=null)
    {
        FinishButton.SetActive(false);
    }

    public void FinishGame()
    {       
        GuiPresenter.FinishGame();
    }

    private void CheckForDescriptionHit()
    {
        //Update inforbar description text with the object below the mouse pointer.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GuiPresenter.UpdateContextInformationText(InfoText, hit);
        }
    }   
}