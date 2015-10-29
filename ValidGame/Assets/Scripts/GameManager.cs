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
   
    public GameObject gameBoard;
    public GameObject contextInfoObject;
    public float cardOffsetY = 0.02f;
    public float contextInfoOffsetY = 0.5f;

    public List<Card> gameCards = new List<Card>();
    public List<Card> placedCards = new List<Card>();
    public Card currentCard;
    private Animator camAnimator;
    private bool gameStarted;
    private float goodCards = 0;
  
    public GameState gameState;
    public PlayingState playingState;
    public GameoverState gameOverState;

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
        playingState = new PlayingState(Instance);
        gameOverState = new GameoverState(Instance);
        gameState = playingState;
        camAnimator = Camera.main.GetComponent<Animator>();
        SpawnCards();
    }
   

    // Update is called once per frame
    void Update()
    {
        if (gameCards.Count <= 0 && gameState != gameOverState)
        {
            gameState = gameOverState;
            DetermineResults();
        }
        gameState.UpdateState();            
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

    private void DetermineResults()
    {
        Card[] cards = FindObjectsOfType<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            SubtopicMatcher matcher = cards[i].GetComponentInParent<SubtopicMatcher>();
            if (matcher && matcher.matchCode == cards[i].matchCode)
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

    public string GetResultString()
    {
        float grade = Mathf.Ceil(goodCards / 6 * 100);
        return "With " + (6 - goodCards) + " errors you score: \n" + grade.ToString() + "%";
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

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
