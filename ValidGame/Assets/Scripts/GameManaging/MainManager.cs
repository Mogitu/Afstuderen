using UnityEngine;
using UnityEngine.SceneManagement;
//Import the developed modules
using AMC.GUI;
using AMC.Networking;
using AMC.Camera;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   A lot of functions can be placed to specific states.
/// </summary>
public class MainManager : MonoBehaviour, IMainManager
{
    //current modules developed for internalship
    public Presenter Presenter;
    public CameraController CameraController;
    public NetworkController NetworkController;

    //other fields
    public GameObject GameBoard;
    public int Score { get; set; }
    public TeamType MyTeamType { get; private set; }
    public EventManager EventManager;

    private GameStateManager GamestateManager;
    public CardController CardController;

    public GameObject GoodPlacementEffect;
    public GameObject WrongPlacementEffect;

    void Awake()
    {
        Application.runInBackground = true;
        MyTeamType = TeamType.CheckAndAct;
        GamestateManager = new GameStateManager(this);
    }

    void Start()
    {
        Score = 0;
        EventManager.AddListener(GameEvents.ReceivedTeamType, OnTeamTypeReceived);
        DisableAllColliders();
    }

    //Update all attached modules if they dont have their own monobehaviour update.
    void Update()
    {
        GamestateManager.UpdateCurrentState();
    }

    public void StartMultiplayerHost()
    {
        NetworkController.StartHosting();
        IsMultiplayerGame = true;
        MyTeamType = TeamType.CheckAndAct;
        CardController.CollectCards();
    }

    public void StartMultiplayerMatch()
    {
        RunGameStartAnimation();
        GamestateManager.SetMultiplayerState();
    }

    public void StartMultiplayerClient()
    {
        string adress = AmcUtilities.ReadFileItem("ip", "config.ini");
        NetworkController.StartClient(adress);
        IsMultiplayerGame = true;
    }

    private void OnTeamTypeReceived(short eventType, Component sender, object param = null)
    {
        int type = (int)param;
        if (type == 1)
        {
            MyTeamType = TeamType.PlanAndDo;
        }
        else
        {
            MyTeamType = TeamType.CheckAndAct;
        }
        CardController.CollectCards();
    }

    public void StartPracticeRound()
    {
        RunGameStartAnimation();
        GamestateManager.SetPlayingState();
        IsMultiplayerGame = false;
        CardController.CollectCards();
        EnableAllColliders();
    }

    public void EndPracticeGame()
    {
        Presenter.ChangeView(VIEWS.GameovermenuView);
        GamestateManager.SetGameoverState();
        RunGameEndAnimation();
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
        EventManager.PostNotification(GameEvents.SendCardToOpponent, null, param);
    }

    //Played at the start of the game. Moves camera to gameboard and hands camera control over to the player. See the animation statemachine.
    public void RunGameStartAnimation()
    {
        Camera.main.GetComponent<Animator>().SetBool("GameStarted", true);
    }

    public void RunGameEndAnimation()
    {
        Camera.main.GetComponent<CameraController>().enabled = false;
        Camera.main.GetComponent<Animator>().enabled = true;
        Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
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
    public void DisableAllColliders()
    {
        Collider[] cols = FindObjectsOfType<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }
    }

    public void EnableAllColliders()
    {
        Collider[] cols = FindObjectsOfType<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
    }

    public void ToggleAllColliders()
    {
        Collider[] cols = FindObjectsOfType<Collider>();
        foreach (Collider col in cols)
        {
            if (!col.enabled)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
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