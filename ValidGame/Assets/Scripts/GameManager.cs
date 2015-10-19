using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;

public class GameManager : NetworkManager
{
    public static GameManager Instance { get; private set; }

    // Use this for initialization       
    public GameObject goodParticle;
    public GameObject wrongParticle;

    public PopupHandler popupHandler;
    public Text ipAdress;
    public Text chatField;
    public InputField chatInput;

    private GameObject mainCam;
    private Card currentCard;

    public List<Card> gameCards = new List<Card>();
    public List<Card> placedCards = new List<Card>();

    public SubtopicMatcher currentSubTopic;
  
    private Animator camAnimator;
    private bool gameStarted;

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
        mainCam = Camera.main.gameObject;
        camAnimator = mainCam.GetComponent<Animator>();
        for (int i = 0; i < gameCards.Count;i++)
        {
            gameCards[i] = Instantiate(gameCards[i]);
            gameCards[i].gameObject.SetActive(false);
           
        }
        //currentCard = GetCard();
    }
   
    bool MatchCard()
    {
        if (currentCard.matchCode == currentSubTopic.matchCode)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chatField.text += chatInput.text + "\n";
            chatInput.text = "";
            //StringMessage msgs = new StringMessage("testmessage");
            //client.Send();
        }
        //make current follow the cursor
        if (currentCard != null)
        {            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Vector3 newPos = hit.point;
                newPos.y += 0.02f;
                currentCard.transform.position = newPos;

                //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
                if (objectHit.gameObject.tag == "ValidTopic")
                {                    
                    SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
                    //place card
                    if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
                    {
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
                        currentCard.transform.position = topicMatcher.slotA.transform.position;
                        gameCards.Remove(currentCard);
                        placedCards.Add(currentCard);
                        currentCard = null;                  
                    }
                }
            }
        }  
        else if(currentCard ==null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
               if(Input.GetMouseButtonDown(0)&& hit.transform.gameObject.tag=="ValidCard")
               {
                   currentCard = hit.transform.gameObject.GetComponent<Card>();
                   gameCards.Add(currentCard);
                   placedCards.Remove(currentCard);
               }
            }
        }     
    }
    public Card GetCard()
    {
        if (gameCards.Count > 0)
        {
            Card tmp = gameCards[Random.Range(0, gameCards.Count - 1)];
            tmp.gameObject.SetActive(true);
            return tmp;
        }
        return null;
    }

    public void HostGame()
    {
        StartHost();       
        RunAnimation();
        popupHandler.Show("Hosting game");
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

    public void PlaceCard()
    {

    }

    public void PickupCard()
    {

    }

    public void SelectCard()
    {
        currentCard = GetCard();
    }   

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
