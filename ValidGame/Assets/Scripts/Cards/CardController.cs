using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls card behaviour etc.
/// TODO    :   To much responsibilities and hardcoded variables
/// </summary>
public class CardController : MonoBehaviour
{
    public GameObject CardInfoCam;
    public Image CardInfoImage;
    public ArrowScript Arrow;
    public List<Card> PlacedCards { get; private set; }
    private Card CurrentCard;
    private List<Card> CardCollection;
    public MainManager MainManager;
    public EventManager EventManager;
    public GameObject CardPlacementEffect;
    private int CardCount;

    public void Awake()
    {
        Arrow.gameObject.SetActive(false);
        CardInfoCam.SetActive(false);
        CardCollection = new List<Card>();
        PlacedCards = new List<Card>();
        EventManager.AddListener(GameEvents.CardReceivedFromOpponent, OnCardReceivedFromOpponent);
        //testy!
        // CardLoader cardLoader = new CardLoader("/Cards");
    }

    public void OnCardReceivedFromOpponent(short Event_Type, Component Sender, object param = null)
    {
        object[] data = (object[])param;
        GameObject go = GameObject.Find((string)data[0]).gameObject;
        go.transform.position = (Vector3)data[1];
        GameObject go2 = Instantiate(CardPlacementEffect);
        go2.transform.position = go.transform.position;
    }

    //Call every frame in manager class.
    private void Update()
    {
        if (CurrentCard != null)
        {
            SetExtraGuiCard("5a");
            Arrow.RotateDown();
            DragCurrentCard();        
        }
        else if (CurrentCard == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUpSelectedCard();
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "ValidCard")
                {
                    Arrow.transform.position = new Vector3(hit.transform.position.x, Arrow.transform.position.y, hit.transform.position.z);
                    Arrow.gameObject.SetActive(true);
                    CardInfoCam.SetActive(true);
                    Arrow.RotateUp();
                }
                else
                {
                    Arrow.gameObject.SetActive(false);
                    CardInfoCam.SetActive(false);
                }
            }
        }
    }

    private void CheckForGameFinishable()
    {
        if (PlacedCards.Count == CardCount)
        {
            EventManager.PostNotification(GameEvents.GameIsFinishable, this, null);
            //MainManager.EndPracticeGame();
        }
    }

    private void SetGameNotFinishable()
    {
        EventManager.PostNotification(GameEvents.UndoGameFinishable, this, null);
    }

    /// <summary>
    /// Drag the card    
    /// </summary>
    public void DragCurrentCard()
    {
        //make current card follow the cursor
        if (CurrentCard != null)
        {
            CurrentCard.transform.position = new Vector3(CurrentCard.transform.position.x,
                                                                       0.0250f,
                                                                       CurrentCard.transform.position.z);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Vector3 newPos = hit.point;
                newPos.y = 0.24f;
                CurrentCard.transform.position = newPos;
                //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
                if (objectHit.gameObject.tag == "ValidSubTopic")
                {
                    if (!Arrow.gameObject.activeSelf)
                    {
                        Arrow.RotateDown();
                        Arrow.gameObject.SetActive(true);                       
                    }
                    Arrow.transform.position = new Vector3(CurrentCard.transform.position.x, Arrow.transform.position.y, CurrentCard.transform.position.z);

                    SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
                    if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !topicMatcher.Occupied)
                    {
                        DropCurrentCard(topicMatcher);
                    }
                }
                else
                {
                    if (Arrow.gameObject.activeSelf)
                        Arrow.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Drop card
    /// </summary>
    /// <param name="obj"></param>
    public void DropCurrentCard(SubtopicMatcher topicMatcher)
    {
        Arrow.gameObject.SetActive(false);
        Vector3 pos = topicMatcher.transform.position;
        pos.y += 0.0006f;
        CurrentCard.transform.position = pos;
        CurrentCard.transform.parent = topicMatcher.transform;
        topicMatcher.Occupied = true;
        CardCollection.Remove(CurrentCard);
        PlacedCards.Add(CurrentCard);
        //route message to opponent
        GameObject go = Instantiate(CardPlacementEffect);
        go.transform.position = CurrentCard.transform.position;
        if (MainManager.IsMultiplayerGame)
        {
            CardToOpponentMessage msg = new CardToOpponentMessage();
            msg.CardName = CurrentCard.name;
            msg.Position = CurrentCard.transform.position;
            MainManager.SendCardToOppent(msg);
        }
        CurrentCard = null;
        CheckForGameFinishable();
    }

    /// <summary>
    ///Pickup the card below the mouse cursor, if any,
    ///TODO :   Not easy to see which card is selected here, refactor?
    /// </summary>
    public void PickUpSelectedCard()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "ValidCard")
            {
                SetGameNotFinishable();
                Transform objectHit = hit.transform;
                SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponentInParent<SubtopicMatcher>();
                topicMatcher.Occupied = false;
                CurrentCard = hit.transform.gameObject.GetComponent<Card>();
                CurrentCard.transform.parent = null;
                CardCollection.Add(CurrentCard);
                PlacedCards.Remove(CurrentCard);
            }
        }
    }

    //retreives the first card with the matching code
    //TODO: currently does not account for multiple cards with the same match code; the first one is always returned.
    public Card GetCard(string code)
    {
        if (CardCollection.Count > 0)
        {
            for (int i = 0; i < CardCollection.Count; i++)
            {
                if (CardCollection[i].MatchCode == code)
                {
                    return CardCollection[i];
                }
            }
        }
        return null;
    }

    //Executed by the gui handler to pick the current selected card in the card browser.
    public void SelectCard(string code)
    {
        CurrentCard = GetCard(code);
        CardInfoCam.SetActive(true);       
    }

    private void SetExtraGuiCard(string matchCode)
    {
        Debug.Log(matchCode);
        GuiCard[] guiCards = FindObjectsOfType<GuiCard>();        
        for(int i=0;i<guiCards.Length;i++)
        {
            if (guiCards[i].MatchCode == matchCode)
            {                     
                CardInfoImage.sprite= guiCards[i].GetComponentInParent<Image>().sprite;
            }
        }
    }


    //Collects all cards that are created in the scene by the builder module and add them to the card collection.
    public void CollectCards()
    {
        Card[] cards = Object.FindObjectsOfType<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i];
            //collect only cards of the current teamtype
            if (MainManager.MyTeamType == card.TypeOfCard)
            {
                CardCollection.Add(card);
            }
        }
        CardCount = CardCollection.Count;
    }
}
