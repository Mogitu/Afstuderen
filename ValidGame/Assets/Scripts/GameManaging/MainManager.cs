using UnityEngine;
using UnityEngine.SceneManagement;
using AMC.GUI;
using AMC.Networking;
using AMC.Camera;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   A lot of functions can be placed to specific states.
/// </summary>
public class MainManager : MonoBehaviour
{
    public GameObject DefaultRoom;
    //current modules developed for internalship
    public Presenter Presenter;
    public CameraController CameraController;
    public NetworkController NetworkController;

    //other fields
    public GameObject GameBoard;
    public int Score { get; set; }
    public TeamType MyTeamType;// { get; private set; }
    [HideInInspector]
    public float GameTime = 300;
    public EventManager EventManager;
    public CardController CardController;

    public GameObject GoodPlacementEffect;
    public GameObject WrongPlacementEffect;

    private string _PlayerName;
    public string SceneName;

    void Awake()
    {
        Application.targetFrameRate = 60;
        //PlayerPrefs.DeleteAll();
        //Application.runInBackground = true;
        MyTeamType = TeamType.ALL;       
        /*
        string room = AmcUtilities.ReadFileItem("room", "config.ini");
        if (room != "default")
        {
            DefaultRoom.SetActive(false);
            ExternalAssetLoader loader = new ExternalAssetLoader();
            loader.LoadBundle("ExternalAssets/kantoor", "myObjects");
            Instantiate(loader.LoadObject("myObjects", room));
        }
        */
        //GameTime = float.Parse(AmcUtilities.ReadFileItem("gametime", "config.ini"));      
    }

    void Start()
    {
        Score = 0;
        EventManager.AddListener(GameEvents.ReceivedTeamType, OnTeamTypeReceived);
        EventManager.AddListener(GameEvents.SendScore, OnScoreReceived);
        EventManager.AddListener(GameEvents.SendScoreNetwork, OnScoreReceived);
        EventManager.AddListener(GameEvents.StartMultiplayerMatch, StartMultiplayerMatch);
        EventManager.AddListener(GameEvents.RestartGame, RestartGame);
        DisableAllColliders();
    }

    private void OnScoreReceived(short gameEvent, Component sender, object obj)
    {
        Score = (int)obj;
    }

    public void StartMultiplayerMatch(short gameEvent, Component sender, object obj)
    {
        RunGameStartAnimation();
        EventManager.PostNotification(GameEvents.BeginMultiplayer, this, null);
    }

    public void StartMultiplayerClient(string name)
    {
        PlayerName = name;
        string adress = AmcUtilities.ReadFileItem("ip", "config.ini");
        NetworkController.StartClient(adress);
        IsMultiplayerGame = true;
        EnableAllColliders();
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
        EventManager.PostNotification(GameEvents.BeginPractice, this, null);
        IsMultiplayerGame = false;        
        CardController.CollectCards();             
        EnableAllColliders();
    }

    public void StartPracticeRound(TeamType teamType)
    {
        MyTeamType = teamType;
        StartPracticeRound();
    }

    public void StartPracticeRoundAllCards()
    {
        RunGameStartAnimation();
        EventManager.PostNotification(GameEvents.BeginPractice, this, null);
        IsMultiplayerGame = false;
        CardController.CollectCards();
        EnableAllColliders();
    }

    private void EndPracticeGame()
    {
        Presenter.ChangeView(VIEWS.GameovermenuView);
        CardController.DisableControls();
        EventManager.PostNotification(GameEvents.EndPractice, this);
        RunGameEndAnimation();
    }

    private void EndMultiplayerGame()
    {
        Presenter.ChangeView(VIEWS.GameovermenuView);
        CardController.DisableControls();
        EventManager.PostNotification(GameEvents.EndMultiplayer, this);
        RunGameEndAnimation();
    }

    public void FinishGame()
    {
        if (IsMultiplayerGame)
        {
            EndMultiplayerGame();
        }
        else
        {
            EndPracticeGame();
        }
    }

    public void RestartGame(short eventType, Component sender, object param = null)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    public string PlayerName
    {
        get { return _PlayerName; }
        set
        {
            if (value.Length > 0)
            {
                _PlayerName = value;
            }
            else
            {
                _PlayerName = "John Doe";
            }
        }
    }
}

public enum TeamType
{
    CheckAndAct,
    PlanAndDo,
    ALL
}