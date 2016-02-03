using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Controls card behaviour etc.
/// TODO    :   To much responsibilities? Decouple drag, drop, pickup BEHAVIOURS?
/// </summary>
public class CardController : ICardController
{
    public Card currentCard;
    public List<Card> placedCards;
    private List<Card> cardCollection;
    //private float cardOffsetY;
    private MainManager mainManager;

    public CardController(MainManager manager)
    {
        cardCollection = new List<Card>();
        placedCards = new List<Card>();
        CollectCards();       
        //cardOffsetY = 0.2f;
        mainManager = manager;
    }

    //Call every frame in manager class.
    public void ManageCards()
    {
        if(cardCollection.Count >0)
        {
            if (currentCard != null)
            {
                DragCurrentCard();
            }
            else if (currentCard == null && Input.GetMouseButtonDown(0))
            {
                PickUpSelectedCard();
            }

            if (cardCollection.Count <= 0)
            {
                mainManager.EndPracticeGame();
            }
        }       
    }


    /// <summary>
    /// Drag the card
    /// TODO    :   Get rid of hardcoded literals.
    /// </summary>
    public void DragCurrentCard()
    {
        //make current card follow the cursor
        if (currentCard != null)
        {
            currentCard.transform.position = new Vector3(currentCard.transform.position.x,
                                                                       0.0250f,
                                                                       currentCard.transform.position.z);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Vector3 newPos = hit.point;
                newPos.y = 0.24f;// cardOffsetY; //gameManager.cardOffsetY;
                currentCard.transform.position = newPos;               
                
                //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
                if (objectHit.gameObject.tag == "ValidTopic")
                {
                    DropCurrentCard(objectHit.gameObject);
                }                
            }
        }       
    }

    /// <summary>
    /// Drop card
    /// </summary>
    /// <param name="obj"></param>
    public void DropCurrentCard(GameObject obj)
    {
        SubtopicMatcher topicMatcher = obj.gameObject.GetComponent<SubtopicMatcher>();
        //place card
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !topicMatcher.occupied)
        {
            Vector3 pos = topicMatcher.transform.position;
            pos.y += 0.0006f;
            currentCard.transform.position = pos;
            currentCard.transform.parent = topicMatcher.transform;
            topicMatcher.occupied = true;
            cardCollection.Remove(currentCard);
            placedCards.Add(currentCard);
            currentCard = null;
        }
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
                Transform objectHit = hit.transform;
                SubtopicMatcher tm = objectHit.gameObject.GetComponentInParent<SubtopicMatcher>();
                tm.occupied = false;
                currentCard = hit.transform.gameObject.GetComponent<Card>();
                currentCard.transform.parent = null;
                cardCollection.Add(currentCard);
                placedCards.Remove(currentCard);
            }
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
        currentCard = GetCard(code);
        currentCard.gameObject.SetActive(true);
    }

    //Collects all cards that are created in the scene by the builder module and add them to the card collection.
    private void CollectCards()
    {
        Card[] cards = Object.FindObjectsOfType<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            Card card = cards[i];
            card.gameObject.SetActive(false);
            cardCollection.Add(card);
        }
    }
}
