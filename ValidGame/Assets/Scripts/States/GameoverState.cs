using UnityEngine;

namespace VALIDGame
{
    public class GameoverState : GameState
    {
        private bool firstRun = false;
        private int goodCards = 0;

        public GameoverState(GameManager manager)
            : base(manager)
        {
        }

        public override void UpdateState()
        {
            //Only run this once
            if (!firstRun)
            {
                DetermineResults();
                firstRun = true;
                //Disable all scripts on the gameboard and camera to avoid interactivity with them(outlines etc)
                MonoBehaviour[] scripts = gameManager.gameBoard.GetComponentsInChildren<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false;
                }
                Camera.main.GetComponent<CameraController>().enabled = false;
                Camera.main.GetComponent<Animator>().enabled = true;
                Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
            }
        }

        public void DetermineResults()
        {
            //Retreive all subtopicmatchers in the placed cards parents and check if their codes match
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
            return "With " + (goodCards) + " good card(s) you score: \n" + score.ToString();
        }
    }
}



