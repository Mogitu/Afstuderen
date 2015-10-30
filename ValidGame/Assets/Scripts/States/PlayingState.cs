﻿using UnityEngine;


public class PlayingState:GameState
{
    public PlayingState(GameManager manager):base(manager)
    {
        
        
    }
    public override void UpdateState()
    {        
       //make current card follow the cursor
       if (gameManager.currentCard != null)
       {
           RaycastHit hit;
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (Physics.Raycast(ray, out hit))
           {
               Transform objectHit = hit.transform;
               Vector3 newPos = hit.point;
               newPos.y += gameManager.cardOffsetY;
               gameManager.currentCard.transform.position = newPos;

               //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
               if (objectHit.gameObject.tag == "ValidTopic")
               {
                   SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
                   //place card
                   if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !topicMatcher.occupied)
                   {
                       gameManager.currentCard.transform.position = topicMatcher.transform.position;
                       gameManager.currentCard.transform.parent = topicMatcher.transform;
                       topicMatcher.occupied = true;
                       gameManager.gameCards.Remove(gameManager.currentCard);
                       gameManager.placedCards.Add(gameManager.currentCard);
                       gameManager.currentCard = null;
                   }
               }
           }
       }
       else if (gameManager.currentCard == null)
       {
           RaycastHit hit;
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (Physics.Raycast(ray, out hit))
           {
               if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.tag == "ValidCard")
               {
                   Transform objectHit = hit.transform;
                   SubtopicMatcher tm = objectHit.gameObject.GetComponentInParent<SubtopicMatcher>();
                   tm.occupied = false;
                   gameManager.currentCard = hit.transform.gameObject.GetComponent<Card>();
                   gameManager.currentCard.transform.parent = null;
                   gameManager.gameCards.Add(gameManager.currentCard);
                   gameManager.placedCards.Remove(gameManager.currentCard);                    
               }
           }
       }
        
    }
}
