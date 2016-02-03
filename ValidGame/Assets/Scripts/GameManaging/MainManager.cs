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
    public GuiPresenter guiPresenter;
    public CameraController cameraController; 
    public GameObject gameBoard;
    public int score;

    private GameStateManager gamestateManager;
    private CardController cardController;//This should be placed somewhere else....probably a state?
    private bool isMultiplayerGame;
    // private NetworkManager networkManager;
    public NetworkController networkController;
    public EventManager eventManager;

    void Awake()
    {
        gamestateManager = new GameStateManager(this);
        cardController = new CardController(this);            
    }

    void Start()
    {
        score = 0;        
    }

    //Update all attached modules if they dont have their own monobehaviour update.
    void Update()
    {
        gamestateManager.UpdateCurrentState();
        cardController.ManageCards();       
    }      

    public void StartMultiplayerHost(string ip)
    {       
        networkController.BeginHosting();
        isMultiplayerGame = true;
    }   

    public void StartMultiplayerMatch()
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
        cardController.SelectCard(code);
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
        get { return cardController; }
    }      

    public bool IsMultiplayerGame
    {
        get { return isMultiplayerGame; }
    }
}
