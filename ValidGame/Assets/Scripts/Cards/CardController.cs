using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls card behaviour etc.
/// TODO    :   To much responsibilities and hardcoded variables.
///             Place context stuff in own class.
///             Needs a heavy refactor.
/// </summary>
public class CardController : MonoBehaviour
{
    //context stuff; should be place in own class  
    public GameObject CardInfoCam;
    public Dictionary<string, Sprite> ContextCards;
    public Image CardInfoImage;
    public ArrowScript Arrow;   
    private float ContextTimer = 0.0f;
    private float MaxContextTime = 0.5f;
    private bool StartContextInfoTimer = false;
    private bool ControlsDisabled = false;
    //end

    public Dictionary<string, Card> PlacedCards { get; private set; }
    private Card _CurrentCard;
    private Dictionary<string, Card> CardCollection;
    public MainManager MainManager;
    public EventManager EventManager;
    public GameObject CardPlacementEffect;
    private int CardCount;


    public Card CurrentCard { get { return _CurrentCard; } }

    public void Awake()
    {
        Arrow.gameObject.SetActive(false);
        CardInfoCam.SetActive(false);
        CardCollection = new Dictionary<string, Card>();
        PlacedCards = new Dictionary<string, Card>();
        EventManager.AddListener(GameEvents.CardReceivedFromOpponent, OnCardReceivedFromOpponent);
        EventManager.AddListener(GameEvents.RequestCardCount, OnCardCountRequested);
        EventManager.AddListener(GameEvents.PickupCard, OnSelectCard);
        EventManager.AddListener(GameEvents.CancelCardSelection, OnCancelCardSelection);

        EventManager.AddListener(GameEvents.EndPractice, OnEndPractice);
        //testy!
        //CardLoader cardLoader = new CardLoader("/Cards");        
    }

    private void OnCancelCardSelection(short eventType, Component sender, object param)
    {
        CurrentCard.transform.position = new Vector3(1000,1000,1000);
        CurrentCard.transform.parent = null;
        _CurrentCard = null;
    }

    private void OnEndPractice(short eventType, Component sender, object param)
    {
        if(_CurrentCard!=null)
        {
            CurrentCard.gameObject.SetActive(false);
            _CurrentCard = null;
        }
      
    }

    private void OnCardCountRequested(short eventType, Component sender, object param)
    {
        EventManager.PostNotification(GameEvents.CardCount, this, CardCount);
    }

    public void OnCardReceivedFromOpponent(short Event_Type, Component Sender, object param = null)
    {
        object[] data = (object[])param;
        GameObject go = GameObject.Find((string)data[0]).gameObject;
        go.transform.position = (Vector3)data[1];
        GameObject go2 = Instantiate(CardPlacementEffect);
        go2.transform.position = go.transform.position;
    }

    public void DisableControls()
    {
        ControlsDisabled = true;
        Arrow.gameObject.SetActive(false);

    }

    //Call every frame in manager class.
    //TODO: divide this method in to smaller methods for readability
    private void Update()
    {
        //avoids that cards can be picked up when mouse click hits both browser button and a placed card.
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (_CurrentCard != null)
        {
            Arrow.RotateDown();
            DragCurrentCard();
        }
        else if (_CurrentCard == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUpSelectedCard();
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                HandleContextInfo(hit);
            }
        }
    }

    private void HandleContextInfo(RaycastHit hit)
    {
        if (hit.transform.gameObject.tag == "ValidCard" && (hit.transform.GetComponent<Card>().TypeOfCard == MainManager.MyTeamType || MainManager.MyTeamType == TeamType.ALL))
        {
            if (!ControlsDisabled)
            {
                Arrow.transform.position = new Vector3(hit.transform.position.x, Arrow.transform.position.y, hit.transform.position.z);
                Arrow.gameObject.SetActive(true);
                Arrow.RotateUp();
            }
            StartContextInfoTimer = true;
        }
        else
        {
            StartContextInfoTimer = false;
            ContextTimer = 0.0f;
            Arrow.gameObject.SetActive(false);
            CardInfoCam.SetActive(false);
        }
        if (StartContextInfoTimer)
        {
            RunContextTimer(hit);
        }
    }

    private void RunContextTimer(RaycastHit hit)
    {
        ContextTimer += Time.deltaTime;
        if (ContextTimer >= MaxContextTime)
        {
            // set the visiable card for the context info card 
            CardInfoCam.SetActive(true);
            Card card = hit.transform.gameObject.GetComponent<Card>();
            if (card)
            {
                SetExtraGuiCard(card.MatchCode);
                StartContextInfoTimer = false;
                ContextTimer = 0;
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

    private Vector3 CurrentCardDragPosition()
    {
        Plane groundPlane = new Plane(Vector3.up, new Vector3(-0.247f, 0.2244f, -0.266f));
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        Vector3 newPos = new Vector3();
        if (groundPlane.Raycast(ray2, out rayDistance))
        {
            newPos = ray2.GetPoint(rayDistance);
            newPos.y = 0.245f;
        }
        return newPos;
    }

    /// <summary>
    /// Drag the card    
    /// </summary>
    public void DragCurrentCard()
    {
        //make current card follow the cursor
        if (_CurrentCard != null)
        {
            if (_CurrentCard.GetComponent<BoxCollider>().enabled)
                _CurrentCard.GetComponent<BoxCollider>().enabled = false;

            _CurrentCard.transform.position = CurrentCardDragPosition();
            //CurrentCard.transform.position = new Vector3(CurrentCard.transform.position.x,
            //0.0250f,
            //CurrentCard.transform.position.z);           

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                ControlDragging(hit);
            }
        }
    }

    private void ControlDragging(RaycastHit hit)
    {
        Transform objectHit = hit.transform;
        //Vector3 newPos = hit.point;
        //newPos.y = 0.24f;
        //CurrentCard.transform.position = newPos;
        //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
        if (objectHit.gameObject.tag == "ValidSubTopic")
        {
            if (!Arrow.gameObject.activeSelf)
            {
                Arrow.RotateDown();
                Arrow.gameObject.SetActive(true);
            }
            Arrow.transform.position = new Vector3(_CurrentCard.transform.position.x, Arrow.transform.position.y, _CurrentCard.transform.position.z);

            SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !topicMatcher.Occupied)
            {
                DropCurrentCard(topicMatcher);
            }
        }
        else if (Arrow.gameObject.activeSelf)
        {
            Arrow.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Drop card
    /// </summary>
    /// <param name="obj"></param>
    public void DropCurrentCard(SubtopicMatcher topicMatcher)
    {
        _CurrentCard.GetComponent<BoxCollider>().enabled = true;
        Arrow.gameObject.SetActive(false);
        Vector3 pos = topicMatcher.transform.position;
        pos.y += 0.0006f;
        _CurrentCard.transform.position = pos;
        _CurrentCard.transform.parent = topicMatcher.transform;
        topicMatcher.Occupied = true;
        CardCollection.Remove(_CurrentCard.MatchCode);
        PlacedCards.Add(_CurrentCard.MatchCode, _CurrentCard);
        //route message to opponent
        GameObject go = Instantiate(CardPlacementEffect);
        go.transform.position = _CurrentCard.transform.position;
        if (MainManager.IsMultiplayerGame)
        {
            CardToOpponentMessage msg = new CardToOpponentMessage();
            msg.CardName = _CurrentCard.name;
            msg.Position = _CurrentCard.transform.position;
            EventManager.PostNotification(GameEvents.SendCardToOpponent, this, msg);
        }
        _CurrentCard = null;
        CheckForGameFinishable();
        EventManager.PostNotification(GameEvents.DisableCardPlaceBack,this,null);
    }

    /// <summary>
    ///Pickup the card below the mouse cursor, if any,
    ///TODO :   Not easy to see which card is selected here, refactor?
    /// </summary>
    public void PickUpSelectedCard()
    {
        if (!ControlsDisabled)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "ValidCard" && (hit.transform.GetComponent<Card>().TypeOfCard == MainManager.MyTeamType || MainManager.MyTeamType == TeamType.ALL))
                {
                    CardInfoCam.SetActive(true);
                    SetGameNotFinishable();
                    Transform objectHit = hit.transform;
                    SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponentInParent<SubtopicMatcher>();
                    topicMatcher.Occupied = false;
                    _CurrentCard = hit.transform.gameObject.GetComponent<Card>();
                    _CurrentCard.transform.parent = null;
                    
                    CardCollection.Add(_CurrentCard.MatchCode, _CurrentCard);
                    PlacedCards.Remove(_CurrentCard.MatchCode);
                    SetExtraGuiCard(_CurrentCard.MatchCode);
                }
            }
            EventManager.PostNotification(GameEvents.EnableCardPlaceBack, this);
        }
    }

   

    //retreives the first card with the matching code
    //TODO: currently does not account for multiple cards with the same match code; the first one is always returned.
    public Card GetCard(string code)
    {
        if (CardCollection.Count > 0)
        {
            return CardCollection[code];
        }
        return null;
    }

    public void OnSelectCard(short gameEvent, Component Sender, object param)
    {
        string code = ((string)param).Trim();
        SetExtraGuiCard(code);
        _CurrentCard = GetCard(code);
        CardInfoCam.SetActive(true);
    }

    private void SetExtraGuiCard(string matchCode)
    {
        CardInfoImage.sprite = ContextCards[matchCode];
    }

    /// <summary>
    /// Fill dictionary with available guicards.
    /// </summary>
    /// <returns>Dictionary with all guicards that are in the scene.</returns>
    private Dictionary<string, Sprite> FillContextCards()
    {
        Dictionary<string, Sprite> tmpDic = new Dictionary<string, Sprite>();
        foreach (KeyValuePair<string, Card> card in CardCollection)
        {
            tmpDic.Add(card.Value.MatchCode, card.Value.GetComponentInChildren<SpriteRenderer>().sprite);
        }
        return tmpDic;
    }

    //Collects all cards that are created in the scene by the builder module and add them to the card collection.
    public void CollectCards()
    {
        Card[] cards = FindObjectsOfType<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i];
            //collect only cards of the current teamtype
            if (MainManager.MyTeamType == card.TypeOfCard)
            {
                AddCardToCollection(card);
            }
            else if (MainManager.MyTeamType == TeamType.ALL)
            {
                AddCardToCollection(card);
            }
        }
        CardCount = CardCollection.Count;
        ContextCards = FillContextCards();

    }

    private void AddCardToCollection(Card card)
    {
        //check if card already exists in dictionary, if so we append the code with 2 at the end
        //if()
        if (CardCollection.ContainsKey(card.MatchCode))
        {
            card.MatchCode = card.MatchCode + "2";
            card.name = "SceneCard" + card.MatchCode;
            CardCollection.Add(card.MatchCode, card);
        }
        else
        {
            //card.MatchCode = card.MatchCode;
            //card.name = "SceneCard" + card.MatchCode;
            CardCollection.Add(card.MatchCode, card);
        }
    }
}