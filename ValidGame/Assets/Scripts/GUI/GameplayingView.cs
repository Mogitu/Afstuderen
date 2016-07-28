using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default gui view for when the player is hovering over the game board.
/// </summary>
public class GameplayingView : View
{
    public GameObject MenuButtons;
    public GameObject FinishButton;
    public GameObject InfoBar;
    public Text InfoText;
    public Text TimerText;
    public Text CardPanelText;
    private GuiPresenter GuiPresenter;
    private MainManager MainManager;
    private bool TimerIsPaused; 

    void Awake()
    {
        FinishButton.SetActive(false);
        GuiPresenter = GetPresenterType<GuiPresenter>();
        GuiPresenter.EventManager.AddListener(GameEvents.GameIsFinishable, OnGameIsFinishable);
        GuiPresenter.EventManager.AddListener(GameEvents.UndoGameFinishable, OnUndoGameFinishable);
        GuiPresenter.EventManager.AddListener(GameEvents.RestartTimer, OnRestartTimer);
        GuiPresenter.EventManager.AddListener(GameEvents.EnableCardPlaceBack, OnPlaceBackEnabled);
        MainManager = GuiPresenter.MainManager;

        MenuButtons.SetActive(false);        
        
    }

    private void OnPlaceBackEnabled(short eventType, Component sender, object param)
    {

       // var animator = GetViewComponent("CardPanel").GetComponent<Animator>();
      //  animator.SetBool("CardPanelClicked", false);
        //animator.SetBool("CanPlaceCardBack", true);
    }

    private void OnRestartTimer(short eventType, Component sender, object param)
    {
        ToggleTimer();
    }

    public void ToggleMenu()
    {
        MenuButtons.SetActive(!MenuButtons.activeSelf);
    }

    public void OpenCardView()
    {
        if (GuiPresenter.MainManager.CardController.CurrentCard==null)
        {
            ToggleTimer();
            GuiPresenter.ToggleCardbrowser();

            var animator = GetViewComponent("CardPanel").GetComponent<Animator>();
            animator.SetBool("CardPanelClicked",true);
            CardPanelText.text = "Click to place card back.";
        }
        else
        {
            GuiPresenter.EventManager.PostNotification(GameEvents.CancelCardSelection,this, GuiPresenter.MainManager.CardController.CurrentCard.MatchCode);
        }     
    }

    public void ToggleOptionsView()
    {
        ToggleMenu();
        GuiPresenter.ToggleOptionsView();
    }

    public void ToggleTimer()
    {
        TimerIsPaused = !TimerIsPaused;
    }

    public void ToggleTutorial()
    {
        ToggleMenu();
        GuiPresenter.ToggleTutorial();
    }

    void FixedUpdate()
    {
        CheckForDescriptionHit();
    }

    void Update()
    {
        if (!TimerIsPaused)
        {
            MainManager.GameTime -= Time.deltaTime;
            MainManager.TimeUsed += Time.deltaTime;
        }
       
        if (MainManager.GameTime <= 0)
        {
            // GuiPresenter.EventManager.PostNotification(GameEvents.EndPractice,null);
            GuiPresenter.FinishGame();
        }

        int minutes = (int)(MainManager.GameTime / 60);
        string seconds = ((int)(MainManager.GameTime % 60)).ToString();

        if (seconds.Length == 1)
        {
            TimerText.text = minutes + ":0" + seconds;
        }
        else
        {
            TimerText.text = minutes + ":" + seconds;
        }

       
        if (CheckForDescriptionHit())
        {
            return;
        }
        if (InfoText.text != "")
        {
            InfoText.text = "";
        }      
    }

    public void Restart()
    {
        ToggleMenu();
        GuiPresenter.Restart();
    }

    public void OnGameIsFinishable(short gameEvent, Component sender, object param = null)
    {
        FinishButton.SetActive(true);
    }

    public void OnUndoGameFinishable(short gameEvent, Component sender, object param = null)
    {
        FinishButton.SetActive(false);
    }

    public void FinishGame()
    {
        GuiPresenter.FinishGame();
    }

    private bool CheckForDescriptionHit()
    {
        //Update inforbar description text with the object below the mouse pointer.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.transform.name != "gameBoard")
            {
                GuiPresenter.UpdateContextInformationText(InfoText, hit);
                return true;
            }
        }
        return false;
    }
}