using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   A lot of functions can be placed to specific states.
/// </summary>
public class MainManager : MonoBehaviour, IMainManager
{
    public GuiPresenter GuiPresenter;
    public CameraControllerDesktop CameraController;
    public GameObject GameBoard;
    public int Score { get; set; }    
    public TeamType MyTeamType { get; private set; }    
    public ValidNetworkController NetworkController;
    public EventManager EventManager;

    private GameStateManager GamestateManager;
    private CardController _CardController;

    void Awake()
    {
        MyTeamType = TeamType.CheckAndAct;
        GamestateManager = new GameStateManager(this);
        _CardController = new CardController(this, EventManager);        
    }

    void Start()
    {
        Score = 0;
    }

    //Update all attached modules if they dont have their own monobehaviour update.
    void Update()
    {
        GamestateManager.UpdateCurrentState();
        CardController.ManageCards();
    }  

    public void StartMultiplayerHost(string ip)
    {       
        NetworkController.StartHosting();
        IsMultiplayerGame = true;
        MyTeamType = TeamType.CheckAndAct;
        CardController.CollectCards();
    }   

    public void StartMultiplayerMatch()
    {
        CameraController.RunGameStartAnimation();
        GamestateManager.SetMultiplayerState();        
    }

    public void StartMultiplayerClient(string ip)
    {
        CameraController.RunGameStartAnimation();
        GamestateManager.SetMultiplayerState();
        NetworkController.StartClient(ip);
        IsMultiplayerGame = true;
        MyTeamType = TeamType.PlanAndDo;
        CardController.CollectCards();
    }  

    public void StartPracticeRound()
    {
        CameraController.RunGameStartAnimation();
        GamestateManager.SetPlayingState();
        IsMultiplayerGame = false;
        CardController.CollectCards();
    }

    public void EndPracticeGame()
    {
        GuiPresenter.ShowGameOverView();
        GamestateManager.SetGameoverState();
        CameraController.RunGameEndAnimation();
    }

    public void EndMultiplayerGame()
    {
        EndPracticeGame();        
    }

    public void RestartGame()
    {       
        SceneManager.LoadScene("GameScene");
    }

    public void PickCard(string code)
    {
        CardController.SelectCard(code);
    }

    public void SendCardToOppent(CardToOpponentMessage param)
    {
        EventManager.PostNotification(GameEvents.SendCardToOpponent, null,param);
    }
   

    public void ToggleCameraActive()
    {
        if (CameraController.enabled)
        {
            CameraController.enabled = false;
        }
        else
        {
            CameraController.enabled = true;
        }
    }

    //For all colliders in the currently active scene.
    public void ToggleAllColliders()
    {
        Collider[] cols = FindObjectsOfType<Collider>();
        foreach(Collider col in cols)
        {
            if (col.enabled)
            {
                col.enabled = false;
            }
            else
            {
                col.enabled = true;
            }           
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

   
    public CardController CardController
    {
        get { return _CardController; }
    }      

    public bool IsMultiplayerGame
    {
        get;
        private set;
    }
}

public enum TeamType
{
    CheckAndAct,
    PlanAndDo
}
