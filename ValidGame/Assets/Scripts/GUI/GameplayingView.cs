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
        MainManager = GuiPresenter.MainManager;
        MenuButtons.SetActive(false);        
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