using UnityEngine;

    public class PlayingState : GameState
    { 
        public PlayingState(GameManager manager)
            : base(manager){
        }

        //TODO: Replace pos.y offset for inspector property, remove hardcoded values.
        public override void UpdateState()
        {           
            gameManager.Timers["GameTime"].Tick(Time.deltaTime);
            //make current card follow the cursor
            if (gameManager.currentCard != null)
            {
                gameManager.currentCard.transform.position = new Vector3(gameManager.currentCard.transform.position.x,
                                                                          0.0250f,
                                                                          gameManager.currentCard.transform.position.z);
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
                            Vector3 pos = topicMatcher.transform.position;
                            pos.y += 0.0006f;
                            gameManager.currentCard.transform.position = pos;
                            gameManager.currentCard.transform.parent = topicMatcher.transform;
                            topicMatcher.occupied = true;
                            gameManager.cardCollection.Remove(gameManager.currentCard);
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
                        gameManager.cardCollection.Add(gameManager.currentCard);
                        gameManager.placedCards.Remove(gameManager.currentCard);
                    }
                }
            }
            if ((gameManager.GetTimer("GameTime") <= 0) || (gameManager.cardCollection.Count <= 0))
            {
                gameManager.gameState = gameManager.gameOverState;
            }
            int minute = (int)Mathf.Abs(gameManager.Timers["GameTime"].GetTime()/60);
            int seconds = (int)gameManager.Timers["GameTime"].GetTime() % 60;
            gameManager.timerText.text = minute.ToString()+":"+seconds.ToString();           
        }
    }


