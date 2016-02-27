using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   In order to comply better with SOLID, this probably should implement an interface.
///             A lot of functions can be placed to specific states.
/// </summary>
public class MainManager : MonoBehaviour, IMainManager
{
    

    public GuiPresenter GuiPresenter;
    public CameraControllerDesktop CameraController;
    public GameObject GameBoard;
    public int Score;

    private GameStateManager GamestateManager;
    private CardController CardController;//This should be placed somewhere else....probably a state?
    public TeamType MyTeamType { get; private set; }
   
    // private NetworkManager networkManager;
    public ValidNetworkController NetworkController;
    public EventManager EventManager;
    

    
    void Awake()
    {
        MyTeamType = TeamType.CheckAndAct;
        GamestateManager = new GameStateManager(this);
        CardController = new CardController(this);        
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

    private void AddListeners()
    {
        
    }

    public void StartMultiplayerHost(string ip)
    {       
        NetworkController.BeginHosting();
        IsMultiplayerGame = true;
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
    }  

    public void StartPracticeRound()
    {
        CameraController.RunGameStartAnimation();
        GamestateManager.SetPlayingState();
        IsMultiplayerGame = false;
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

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
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

    //TODO  :   Violates LOD in gameoverstate class!
    public CardController CardManager
    {
        get { return CardController; }
    }      

    public bool IsMultiplayerGame
    {
        get;
        set;
    }
}

public enum TeamType
{
    CheckAndAct,
    PlanAndDo
}
