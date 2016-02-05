using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Presenter that is responsible for the main gui and underlying views.
/// TODO    :   When this class grows to big it is an idea to split the gui up with multiple presenters.
                
/// </summary>
public class GuiPresenter : Presenter
{   
    public MainManager mainManager;
    public EventManager eventManager;

    public override void Start()
    {
        base.Start();
        ChangeView(VIEWS.mainmenuView);
        eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, StartMatch);
    }  

    public void StartPracticeRound()
    {
        ChangeView(VIEWS.gamePlayingView);
        mainManager.StartPracticeRound();
    }

    public void StartMultiplayerClient(string ip)
    {
        ChangeView(VIEWS.gamePlayingView);
        OpenView(VIEWS.multiplayerGameplayView);
        mainManager.StartMultiplayerClient(ip);
    }

    //TODO  : start host instead of client.
    public void StartMultiplayerHost(string ip)
    {
        ChangeView(VIEWS.multiplayerGameplayView);
        mainManager.StartMultiplayerHost(ip);
    }

    public void StartMatch(short event_type, Component sender, object Param=null)
    {
        OpenView(VIEWS.gamePlayingView);        
    }

    public void Restart()
    {
        mainManager.RestartGame();
    }

    public void QuitApplication()
    {
        mainManager.QuitApplication();
    }

    public void ToggleCardbrowser()
    {
        mainManager.ToggleCameraActive();
        mainManager.ToggleAllColliders();
        ToggleView(VIEWS.cardbrowserView);      
    }

    public void ShowMainmenuView()
    {
        ChangeView(VIEWS.mainmenuView);        
    }

    public void EndPracticeGame()
    {
        mainManager.EndPracticeGame();
    }

    public void EndMultiplayerGame()
    {
        mainManager.EndMultiplayerGame();
    }

    //TODO  :   Reduce coupling between mainmanager, this presenter and the cardcontroller.
    public void PickCard(string code)
    {
        ToggleCardbrowser();
        mainManager.PickCard(code);
    }

    public void ShowGameOverView()
    {
        ChangeView(VIEWS.gameovermenuView);        
    }

    public void ShowMultiplayerView()
    {
        ChangeView(VIEWS.multiplayermenuView);
    }    

    public void PostChatSend(string str)
    {
        eventManager.PostNotification(EVENT_TYPE.SENDCHAT, this, str);       
    }     

    public void PostScoreSend(string str)
    {
        eventManager.PostNotification(EVENT_TYPE.SENDSCORE, this, str);
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
            txt.text = desc.descriptionTxt.text;
        }
    }
}