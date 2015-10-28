using UnityEngine;


public class PlayingState:GameState
{
    public PlayingState()
    {

    }
    public override void UpdateState(GameObject gameObject)
    {
        
       //make current card follow the cursor
       if (GameManager.Instance.currentCard != null)
       {
           RaycastHit hit;
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           if (Physics.Raycast(ray, out hit))
           {
               Transform objectHit = hit.transform;
               Vector3 newPos = hit.point;
               newPos.y += GameManager.Instance.cardOffsetY;
               GameManager.Instance.currentCard.transform.position = newPos;

               //If the card hovers over an topic we query the topic data and place the card when the card is clicked.           
               if (objectHit.gameObject.tag == "ValidTopic")
               {
                   SubtopicMatcher topicMatcher = objectHit.gameObject.GetComponent<SubtopicMatcher>();
                   //place card
                   if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1) && !topicMatcher.occupied)
                   {
                       GameManager.Instance.currentCard.transform.position = topicMatcher.transform.position;
                       GameManager.Instance.currentCard.transform.parent = topicMatcher.transform;
                       topicMatcher.occupied = true;
                       GameManager.Instance.gameCards.Remove(GameManager.Instance.currentCard);
                       GameManager.Instance.placedCards.Add(GameManager.Instance.currentCard);
                       GameManager.Instance.currentCard = null;
                   }
               }
           }
       }
       else if (GameManager.Instance.currentCard == null)
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
                   GameManager.Instance.currentCard = hit.transform.gameObject.GetComponent<Card>();
                   GameManager.Instance.currentCard.transform.parent = null;
                   GameManager.Instance.gameCards.Add(GameManager.Instance.currentCard);
                   GameManager.Instance.placedCards.Remove(GameManager.Instance.currentCard);
                    
               }
           }
       }
        
    }
}

