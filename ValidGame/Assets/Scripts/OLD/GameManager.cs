using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;
using AMCTools;

/**
 * PRELIMINARY GAMEMANAGER:
 * Handles all game specific tasks, most tasks can be delegated to individual modules which will be developed later. *  
 **/
namespace VALIDGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } //Singleton instance    
        public GameObject goodParticle;
        public GameObject wrongParticle;
        public PopupHandler popupHandler;
        public Text timerText;
        public float maxGameTime = 60.0f;
        public float cardOffsetY = 0.2f;
        public GameState gameState;
        public PlayingState playingState;
        public GameoverState gameOverState;
        public WaitingState waitingState;

        public GameObject gameBoard;
        public float score = 0;
        public string scoreText;

        public List<Card> cardCollection = new List<Card>();
        public List<Card> placedCards = new List<Card>();
        public Card currentCard;       
 
        private bool gameStarted;    
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
            playingState = new PlayingState(Instance);
            gameOverState = new GameoverState(Instance);
            waitingState = new WaitingState(Instance);
            RegisterTimer("GameTime");
            UpdateTimer("GameTime", maxGameTime);      
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
            RunAnimation();
            popupHandler.Show("Practice");
            gameState = playingState;
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
            Camera.main.GetComponent<Animator>().SetBool("GameStarted", true);
        }

        public Dictionary<string, Timer> Timers
        {
            get { return timers; }
            set { timers = value; }
        }
    }
}
