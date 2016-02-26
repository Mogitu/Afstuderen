using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Presenter that is responsible for the main gui and underlying views.
/// TODO    :   When this class grows to big it is an idea to split the gui up with multiple presenters.

/// </summary>
public class GuiPresenter : Presenter
{   
    public MainManager MainManager;
    public EventManager EventManager;

    public  void Start()
    {        
        ChangeView(VIEWS.MainmenuView);
        EventManager.AddListener(GameEvents.PlayerJoined, StartMatch);
    }  

    public void StartPracticeRound()
    {
        ChangeView(VIEWS.GamePlayingView);
        MainManager.StartPracticeRound();
    }

    public void StartMultiplayerClient(string ip)
    {
        ChangeView(VIEWS.GamePlayingView);
        OpenView(VIEWS.MultiplayerGameplayView);
        MainManager.StartMultiplayerClient(ip);
    }

    //TODO  : start host instead of client.
    public void StartMultiplayerHost(string ip)
    {
        ChangeView(VIEWS.MultiplayerGameplayView);
        MainManager.StartMultiplayerHost(ip);
    }

    public void StartMatch(short event_type, Component sender, object Param=null)
    {
        OpenView(VIEWS.GamePlayingView);        
    }

    public void Restart()
    {
        MainManager.RestartGame();
    }

    public void QuitApplication()
    {
        MainManager.QuitApplication();
    }

    public void ToggleCardbrowser()
    {
        MainManager.ToggleCameraActive();
        MainManager.ToggleAllColliders();
        ToggleView(VIEWS.CardbrowserView);      
    }

    public void ShowMainmenuView()
    {
        ChangeView(VIEWS.MainmenuView);        
    }

    public void EndPracticeGame()
    {
        MainManager.EndPracticeGame();
    }

    public void EndMultiplayerGame()
    {
        MainManager.EndMultiplayerGame();
    }

    //TODO  :   Reduce coupling between mainmanager, this presenter and the cardcontroller.
    public void PickCard(string code)
    {
        ToggleCardbrowser();
        MainManager.PickCard(code);
    }

    public void ShowGameOverView()
    {
        ChangeView(VIEWS.GameovermenuView);        
    }

    public void ShowMultiplayerView()
    {
        ChangeView(VIEWS.MultiplayermenuView);
    }    

    public void PostChatSend(string str)
    {
        EventManager.PostNotification(GameEvents.SendSchat, this, str);       
    }     

    public void PostScoreSend(string str)
    {
        EventManager.PostNotification(GameEvents.SendScore, this, str);
    }

    /// <summary>
    /// Update a text with the topic description on an object, if existing.
    /// </summary>
    /// <param name="txt">The text mesh to update.</param>
    /// <param name="hit">The hit to use.</param>
    public void UpdateContextInformationText(Text txt, RaycastHit hit)
    {
        ContextDescription desc = hit.transform.gameObject.GetComponentInChildren<ContextDescription>();
        if (desc != null)
        {
            txt.text = desc.DescriptionText;
        }
    }
}