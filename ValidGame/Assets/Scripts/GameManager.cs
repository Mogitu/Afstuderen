using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;


/**
 * PRELIMINARY GAMEMANAGER:
 * Handles all game specific tasks, most tasks can be delegated to individual modules which will be developed later. *  
 **/

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //Singleton instance    
    public GameObject goodParticle;
    public GameObject wrongParticle;
    public PopupHandler popupHandler;
    public Text timerText;
    public float maxGameTime=60.0f;
    public float cardOffsetY = 0.2f;
   
    public GameObject gameBoard;
    public float score = 0;
    public string scoreText;

    public List<Card> gameCards = new List<Card>();
    public List<Card> placedCards = new List<Card>();
    public Card currentCard;
    private Animator camAnimator;
    private bool gameStarted;   
  
    public GameState gameState;
    public PlayingState playingState;
    public GameoverState gameOverState;
    public WaitingState waitingState;

    private Dictionary<string, Timer> timers = new Dictionary<string, Timer>();   

    void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RegisterTimer("GameTime");
        UpdateTimer("GameTime", maxGameTime);
        playingState = new PlayingState(Instance);
        gameOverState = new GameoverState(Instance);
        waitingState = new WaitingState(Instance);
        gameState = playingState;
        camAnimator = Camera.main.GetComponent<Animator>();
        SpawnCards();
    }
   

    // Update is called once per frame
    void Update()
    {      
        gameState.UpdateState();
        // Update all timers
        foreach (KeyValuePair<string, Timer> entry in timers)
        {
            entry.Value.Tick(Time.deltaTime);            
        }        
    }    
  
    public void DisableEnableScripts(GameObject gameObject, bool status)
    {
        MonoBehaviour[] scripts = gameObject.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = status;
        }
    }

    private void SpawnCards()
    {
        Card[] cards = Resources.LoadAll<Card>("Gamecards/Scene");
        for (int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i];

            card = Instantiate(card);
            card.gameObject.SetActive(false);
            gameCards.Add(card);
        }
    }

    public Card GetCard(string code)
    {
        if (gameCards.Count > 0)
        {
            for (int i = 0; i < gameCards.Count; i++)
            {
                if (gameCards[i].matchCode == code)
                {
                    return gameCards[i];
                }
            }
        }
        return null;
    }  

   

    public void SelectCard(string code)
    {
        GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard, true);
        currentCard = GetCard(code);
        currentCard.gameObject.SetActive(true);
        //gameState = GAMESTATE.PLACING;
    }

    public void StartPractice()
    {
        RunAnimation();
        popupHandler.Show("Practice");
    }

    // ------------------------------------------------------------------------------------------
    // Name    :    RegisterTimer
    // Desc    :    Can be called by any object to register a special timer with the game manager
    //            with the specified name.
    //    -----------------------------------------------------------------------------------------
    public void RegisterTimer(string key)
    {
        // If a timer with this name does no already exist in
        // our hash table
        if (!timers.ContainsKey(key))
        {
            // Store the name and the index of the timer in the dictionary table
            timers.Add(key, new Timer());
        }
    }

    // ------------------------------------------------------------------------------------
    // Name    :    GetTimer
    // Desc    :    Get value of timer
    // ------------------------------------------------------------------------------------
    public float GetTimer(string key)
    {
        Timer timer = null;

        // Does a timer exist with the requested name
        if (timers.TryGetValue(key, out timer))
        {
            // Return its time
            return timer.GetTime();
        }

        // No timer found
        return -1.0f;
    }

    // ------------------------------------------------------------------------------------
    // Name    :    UpdateTimer
    // Desc    :   
    // ------------------------------------------------------------------------------------
    public void UpdateTimer(string key, float t)
    {
        Timer timer;
        if (timers.TryGetValue(key, out timer))
        {
            timer.AddTime(t);
        }
    }

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }

    public Dictionary<string, Timer> Timers
    {
        get { return timers; }
        set { timers = value; }
    }    
}
