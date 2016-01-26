using UnityEngine;
using UnityEngine.UI;
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
    public float maxGameTime = 60.0f;
    public float cardOffsetY = 0.2f;
    public GameStateOld gameState;
    public PlayingStateOld playingState;
    public GameoverStateOld gameOverState;
    public WaitingStateOld waitingState;

    public GameObject gameBoard;
    public float score = 0;
    public string scoreText;

    public List<Card> cardCollection = new List<Card>();
    public List<Card> placedCards = new List<Card>();
    public Card currentCard;
    private Dictionary<string, Timer> timers = new Dictionary<string, Timer>();

    //Awake always gets called first upon sceneload and activation etc.
    //We make sure there can be only one manager in the scene.
    void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Start()
    {
        playingState = new PlayingStateOld(Instance);
        gameOverState = new GameoverStateOld(Instance);
        waitingState = new WaitingStateOld(Instance);       
        gameState = waitingState;
        CollectCards();
    }


    // Update is called once per frame
    void Update()
    {
        gameState.UpdateState();
        // Update all timers
        /*
        foreach (KeyValuePair<string, Timer> entry in timers)
        {
            // entry.Value.Tick(Time.deltaTime);            
        }
        */
    }

    //Disable/enable all attached monobehaviours on an object.
    public void DisableEnableScripts(GameObject gameObject, bool status)
    {
        MonoBehaviour[] scripts = gameObject.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = status;
        }
    }

    //Collects all cards that are created in the scene by the builder module and add them to the card collection.
    private void CollectCards()
    {
        Card[] cards = FindObjectsOfType<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i];
            card.gameObject.SetActive(false);
            cardCollection.Add(card);
        }
    }

    //retreives the first card with the matching code
    //TODO: currently does not account for multiple cards with the same match code; the first one is always returned.
    public Card GetCard(string code)
    {
        if (cardCollection.Count > 0)
        {
            for (int i = 0; i < cardCollection.Count; i++)
            {
                if (cardCollection[i].matchCode == code)
                {
                    return cardCollection[i];
                }
            }
        }
        return null;
    }

    //Executed by the gui handler to pick the current selected card in the card browser.
    public void SelectCard(string code)
    {
        GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard, true);
        currentCard = GetCard(code);
        currentCard.gameObject.SetActive(true);
    }

    //Start singleplayer practice
    public void StartPractice()
    {       
        popupHandler.Show("Practice");
        gameState = playingState;
    }

   

    

   
}

