using UnityEngine;



public class GameoverState : GameState
{
    private bool firstRun = false;
    private int goodCards = 0;

    public GameoverState(GameManager  manager):base(manager) {       
    }

    public override void UpdateState()
    {
       if(!firstRun)
       {       
           DetermineResults();
           firstRun = true;
       }        
    }

    public void DetermineResults()
    {
        //Card[] cards = FindObjectsOfType<Card>();
        for (int i = 0; i < gameManager.placedCards.Count; i++)
        {
            SubtopicMatcher matcher = gameManager.placedCards[i].GetComponentInParent<SubtopicMatcher>();
            if (matcher && matcher.matchCode == gameManager.placedCards[i].matchCode)
            {                
                GameObject go = GameObject.Instantiate(gameManager.goodParticle) as GameObject;
                go.transform.position = gameManager.placedCards[i].transform.position;
                gameManager.placedCards[i].GetComponent<Renderer>().material.color = Color.green;
                goodCards++;
            }
            else
            {
                GameObject go = GameObject.Instantiate(gameManager.wrongParticle) as GameObject;
                go.transform.position = gameManager.placedCards[i].transform.position;
                gameManager.placedCards[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
        gameManager.score = Mathf.Ceil(goodCards * 100 + (goodCards * 100 * gameManager.GetTimer("GameTime")));
        gameManager.scoreText = GetResultString(gameManager.score);
    }

    public string GetResultString(float score)
    {        
        return "With " + (goodCards) + " good cards you score: \n" + score.ToString();
    }
}

