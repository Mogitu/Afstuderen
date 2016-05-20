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

    public void Start()
    {
        ChangeView(VIEWS.MainmenuView);
        EventManager.AddListener(GameEvents.StartMultiplayerMatch, StartMatch);     
    }

    public void StartPracticeRound(TeamType teamType)
    {
        ChangeView(VIEWS.GamePlayingView);       
        MainManager.StartPracticeRound(teamType);       
    }

    public void StartPracticeRound()
    {
        ChangeView(VIEWS.GamePlayingView);
        MainManager.MyTeamType = TeamType.ALL;
        MainManager.StartPracticeRoundAllCards();
    }

    public void StartMultiplayerClient(string name)
    {  
        ChangeView(VIEWS.MatchMakerView);
        MainManager.StartMultiplayerClient(name);       
    }

    public void ShowTeamSelectView()
    {
        ChangeView(VIEWS.TeamSelectView);
    }
 
    private void OpenMultiplayerViews()
    {
        ChangeView(VIEWS.GamePlayingView);
        OpenView(VIEWS.MultiplayerGameplayView);
    }    

    public void StartMatch(short event_type, Component sender, object Param = null)
    {
        OpenMultiplayerViews();
    }

    public void Restart()
    {
        EventManager.PostNotification(GameEvents.RestartGame, this);
        //MainManager.RestartGame();
    }

    public void QuitApplication()
    {
        MainManager.QuitApplication();
    }

    public void ToggleCardbrowser()
    {
        MainManager.ToggleAllColliders();
        MainManager.ToggleCameraActive();        
        ToggleView(VIEWS.CardbrowserView);
    }

    public void ShowMainmenuView()
    {
        ChangeView(VIEWS.MainmenuView);
    }

    /*
    public void EndPracticeGame()
    {
        MainManager.EndPracticeGame();
    }

    public void EndMultiplayerGame()
    {
        MainManager.EndMultiplayerGame();
    }
    */
   
    public void PickCard(string code)
    {               
        ToggleCardbrowser();
        EventManager.PostNotification(GameEvents.PickupCard, this, code);
    }

    public void ShowGameOverView()
    {
        ChangeView(VIEWS.GameovermenuView);
    }

    public void ShowMultiplayerView()
    {
        ChangeView(VIEWS.MultiplayermenuView);
    }

    public void ToggleTutorial()
    {
        MainManager.ToggleAllColliders();
        MainManager.ToggleCameraActive();
        ToggleView(VIEWS.TutorialView);
    }

    public void PostChatSend(string str)
    {
        EventManager.PostNotification(GameEvents.SendSchat, this, str);
    }

    public void PostScoreSend(string str)
    {
        EventManager.PostNotification(GameEvents.SendScore, this, str);
    }

    public void OpenOptionView()
    {        
        MainManager.DisableAllColliders();
        ChangeView(VIEWS.OptionsView);
    }  

    public void CloseTutorialView()
    {
        CloseView(VIEWS.TutorialView);
    }

    public void ToggleOptionsView()
    {
        MainManager.ToggleAllColliders();
        MainManager.ToggleCameraActive();
        ToggleView(VIEWS.OptionsView);
        GameObject view = GetView(VIEWS.OptionsView);
        EventManager.PostNotification(GameEvents.UpdateSettings, this, null);
        view.GetComponent<OptionsView>().GetViewComponent("GoBackBtn").SetActive(false);        
    }

    public void FinishGame()
    {
        //MainManager.EndPracticeGame();
        MainManager.FinishGame();
    }

    public TeamType GetTeamType
    {
        get { return MainManager.MyTeamType; }
    }

    /// <summary>
    /// Update a text with the topic description on an object, if existing.
    /// </summary>
    /// <param name="txt">The text mesh to update.</param>
    /// <param name="hit">The hit to use.</param>
    public void UpdateContextInformationText(Text txt, RaycastHit hit)
    {
        ContextDescription desc = hit.transform.gameObject.GetComponentInChildren<ContextDescription>();
        if (desc != null )
        {
            txt.text = desc.DescriptionText;
            return;
        }

        Transform parent = hit.transform.parent;
        desc = parent.transform.gameObject.GetComponentInChildren<ContextDescription>();
        if(desc)
        {
            txt.text = desc.DescriptionText;
        }    
    }   
}