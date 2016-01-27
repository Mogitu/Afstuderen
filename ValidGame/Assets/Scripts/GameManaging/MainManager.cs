using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   In order to comply better with SOLID, this probably should implement an interface.
/// </summary>
public class MainManager : MonoBehaviour
{
    public GuiPresenter guiPresenter;
    public CameraController cameraController; 
    public GameObject gameBoard;
    public int score;

    private GameStateManager gamestateManager;
    private CardManager cardManager;
    private NetworkManager networkManager; 

    void Awake()
    {
        gamestateManager = new GameStateManager(this);
        cardManager = new CardManager(this);      
    }

    void Start()
    {
        score = 0;
    }

    //Update all attached modules if they dont have their own monobehaviour update.
    void Update()
    {
        gamestateManager.UpdateCurrentState();
        cardManager.ManageCards();
        if(networkManager != null)
        {
            networkManager.Update();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            networkManager.SendSocketMessage();
        }
    }

    public void StartMultiplayerHost(string ip)
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetMultiplayerState();
        networkManager = new NetworkManager();
        networkManager.CreateHost(ip);
    }

    public void StartMultiplayerClient()
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetMultiplayerState();
        networkManager = new NetworkManager();
        networkManager.CreateClient();
    }  

    public void StartPracticeRound()
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetPlayingState();
    }

    public void EndGame()
    {
        guiPresenter.ShowGameOverView();
        gamestateManager.SetGameoverState();
        cameraController.RunGameEndAnimation();
    }

    public void Restart()
    {
        if(networkManager != null)
        {
            networkManager.Disconnect();
        }      
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
    public CardManager CardManager
    {
        get { return cardManager; }
    }    
}
