using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class GameManager : NetworkManager
{
    // Use this for initialization     
    public GameObject gameMenu;
    public GameObject mainMenu;
    public PopupHandler popupHandler;
    public Text ipAdress;
    public InputField chatInput;
    public Text chatField;

    public GameObject goodParticle;
    public GameObject wrongParticle;
    public GameObject contextInfo;

    public Card currentCard;
    public SubtopicMatcher currentSubTopic;

    public static GameManager Instance { get; private set; }

    private GameObject mainCam;
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

                // Do something with the object that was hit by the raycast.              
                if (objectHit.gameObject.tag == "ValidTopic")
                {
                    SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
                    if (Input.GetMouseButton(0))
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
                        currentCard.transform.SetParent(topicMatcher.slotA.transform);
                        currentCard.transform.position = topicMatcher.slotA.transform.position;
                        currentCard = null;
                        contextInfo.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            /*
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Vector3 newPos = hit.point;
                newPos.y += 0.02f;
                contextInfo.transform.position = newPos;             
            }
             */
        }
    }

    public void HostGame()
    {
        StartHost();
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
        popupHandler.Show("Hosting game");
    }

    public void JoinGame()
    {
        networkAddress = ipAdress.text;
        StartClient();
        Network.Connect(ipAdress.text, 7777);
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
        popupHandler.Show("Joining game");
    }

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
