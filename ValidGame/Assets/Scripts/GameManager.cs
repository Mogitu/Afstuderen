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

public class GameManager : NetworkManager
{
    public enum GAMESTATE
    {
        PLACING,
        BROWSING,
        INSPECTING,
        GAMEOVER,
    }

    public static GameManager Instance { get; private set; } //Singleton instance
    public GAMESTATE gameState;

    public GameObject goodParticle;
    public GameObject wrongParticle;
    public PopupHandler popupHandler;
    public Text ipAdress;
    public Text chatField;
    public InputField chatInput;
    public GameObject gameBoard;
    public GameObject contextInfoObject;
    public float cardOffsetY = 0.02f;
    public float contextInfoOffsetY = 0.5f;

    private List<Card> gameCards = new List<Card>();
    private List<Card> placedCards = new List<Card>();
    private Card currentCard;
    private Animator camAnimator;
    private bool gameStarted;
    private float goodCards = 0;

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
        gameState = GAMESTATE.PLACING;
        camAnimator = Camera.main.GetComponent<Animator>();
        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chatField.text += chatInput.text + "\n";
            chatInput.text = "";
        }

        if(gameCards.Count<=0 && gameState!= GAMESTATE.GAMEOVER)
        {
            gameState = GAMESTATE.GAMEOVER;
            DetermineResults();
        }

        //handle the different gamestates
        switch (gameState)
        {
            case GAMESTATE.PLACING:
                HandlePlacing();
                break;
            case GAMESTATE.BROWSING:
                HandleBrowsing();
                break;
            case GAMESTATE.INSPECTING:
                HandleInspecting();
                break;
            case GAMESTATE.GAMEOVER:
                HandleGameOver();
                break;
        }
    }

    private void HandlePlacing()
    {
        //make current card follow the cursor
        if (currentCard != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Vector3 newPos = hit.point;
                newPos.y += cardOffsetY;
                currentCard.transform.position = newPos;

                //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
                if (objectHit.gameObject.tag == "ValidTopic")
                {
                    SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
                    //place card
                    if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
                    {
                        /*
                        if (topicMatcher != null && topicMatcher.matchCode == currentCard.matchCode)
                        {
                            GameObject go = Instantiate(goodParticle) as GameObject;
                            go.transform.position = topicMatcher.slotA.transform.position;
                        }
                        else
                        {
                            GameObject go = Instantiate(wrongParticle) as GameObject;
                            go.transform.position = topicMatcher.slotA.transform.position;
                        }
                        */

                       
                            currentCard.transform.position = topicMatcher.slotA.transform.position;
                            currentCard.transform.parent = topicMatcher.slotA.transform;
                                   
                        gameCards.Remove(currentCard);
                        placedCards.Add(currentCard);
                        currentCard = null;                      
                    }
                }
            }
        }
        else if (currentCard == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.tag == "ValidCard")
                {
                    currentCard = hit.transform.gameObject.GetComponent<Card>();
                    currentCard.transform.parent = null;
                    gameCards.Add(currentCard);
                    placedCards.Remove(currentCard);
                }
            }
        }
    }

    private void HandleInspecting()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            contextInfoObject.SetActive(true);
            Transform objectHit = hit.transform;
            Vector3 newPos = hit.point;

            if (objectHit.gameObject.tag == "ValidCard")
            {              
                newPos.y += cardOffsetY;                
                contextInfoObject.transform.position = newPos;
                return;
            }
        }
        contextInfoObject.SetActive(false);
    }

    private void HandleBrowsing()
    {

    }

    private void HandleGameOver()
    {
        
       
    }

    private void DetermineResults()
    {
        Card[] cards = FindObjectsOfType<Card>();
        for(int i=0; i< cards.Length; i++)
        {
            SubtopicMatcher matcher = cards[i].GetComponentInParent<SubtopicMatcher>();
            if(matcher && matcher.matchCode == cards[i].matchCode)
            {
                GameObject go = Instantiate(goodParticle) as GameObject;
                go.transform.position = cards[i].transform.position;
                cards[i].GetComponent<Renderer>().material.color = Color.green;
                goodCards++;
            }
            else
            {                
                GameObject go = Instantiate(wrongParticle) as GameObject;
                go.transform.position = cards[i].transform.position;
                cards[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    private void HandleChat()
    {

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

    public void PlaceCard()
    {

    }

    public void PickupCard()
    {

    }

    public string GetResultString()
    {
        float grade = Mathf.Ceil(goodCards / 6 * 100);       
        return "With "+ (6-goodCards)+" errors you score: \n" +  grade.ToString() +"%";
    }

    public void SelectCard(string code)
    {
        GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard, true);
        currentCard = GetCard(code);
        currentCard.gameObject.SetActive(true);
        gameState = GAMESTATE.PLACING;
    }

    public void StartPractice()
    {
        RunAnimation();
        popupHandler.Show("Practice");
    }

    public void JoinGame()
    {
        networkAddress = ipAdress.text;
        StartClient();
        Network.Connect(ipAdress.text, 7777);
        RunAnimation();
        popupHandler.Show("Joining game");
    }

    public void HostGame()
    {
        StartHost();
        RunAnimation();
        popupHandler.Show("Hosting game");
    }

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
