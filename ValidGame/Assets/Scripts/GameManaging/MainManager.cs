using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   In order to comply better with SOLID, this probably should implement an interface.
/// </summary>
public class MainManager : MonoBehaviour, IMainManager
{
    public GuiPresenter guiPresenter;
    public CameraController cameraController; 
    public GameObject gameBoard;
    public int score;

    private GameStateManager gamestateManager;
    private CardController cardManager;
    private bool isMultiplayerGame;
    // private NetworkManager networkManager;
    public NetworkController networkController;
    public EventManager eventManager;

    void Awake()
    {
        gamestateManager = new GameStateManager(this);
        cardManager = new CardController(this);            
    }

    void Start()
    {
        score = 0;
        eventManager.AddListener(EVENT_TYPE.PLAYERJOINED, OnPlayerJoined);
    }

    //Update all attached modules if they dont have their own monobehaviour update.
    void Update()
    {
        gamestateManager.UpdateCurrentState();
        cardManager.ManageCards();       
    }      

    public void StartMultiplayerHost(string ip)
    {       
        networkController.BeginHosting();
        isMultiplayerGame = true;
    }

    public void OnPlayerJoined(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        StartMultiplayerMatch();
    }

    void StartMultiplayerMatch()
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetMultiplayerState();
    }

    public void StartMultiplayerClient(string ip)
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetMultiplayerState();
        networkController.StartClient(ip);
        isMultiplayerGame = true;
    }  

    public void StartPracticeRound()
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetPlayingState();
        isMultiplayerGame = false;
    }

    public void EndPracticeGame()
    {
        guiPresenter.ShowGameOverView();
        gamestateManager.SetGameoverState();
        cameraController.RunGameEndAnimation();
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
        cardManager.SelectCard(code);
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void ToggleCameraActive()
    {
        if (cameraController.enabled)
        {
            cameraController.enabled = false;
        }
        else
        {
            cameraController.enabled = true;
        }
    }

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
        get { return cardManager; }
    }      

    public bool IsMultiplayerGame
    {
        get { return isMultiplayerGame; }
    }
}
