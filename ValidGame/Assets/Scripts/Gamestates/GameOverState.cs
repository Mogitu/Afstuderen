using UnityEngine;
using AMC.Camera;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   ...
/// </summary>
public class GameOverState : GameState
{
    private bool firstRun = false;
    private int goodCards = 0;

    public GameOverState(MainManager manager)
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
            Camera.main.GetComponent<CameraController>().enabled = false;
            Camera.main.GetComponent<Animator>().enabled = true;
            Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
        }
    }

    /// <summary>
    /// Check how many cards are properly placed.
    /// TODO    :   Violates law of demeter!
    /// </summary>
    public void DetermineResults()
    {
        //Retreive all subtopicmatchers in the placed cards parents and check if their codes match
        for (int i = 0; i < gameManager.CardManager.placedCards.Count; i++)//LOD violation
        {
            SubtopicMatcher matcher = gameManager.CardManager.placedCards[i].GetComponentInParent<SubtopicMatcher>();
            if (matcher && matcher.matchCode == gameManager.CardManager.placedCards[i].matchCode)
            {                
                gameManager.CardManager.placedCards[i].GetComponent<Renderer>().material.color = Color.green;
                goodCards++;
            }
            else
            {              
                gameManager.CardManager.placedCards[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
        Debug.Log(goodCards + " properly placed cards.");
        SendScores();
    }

    public void SendScores()
    {
        if (gameManager.IsMultiplayerGame)
        {
            gameManager.eventManager.PostNotification(EVENT_TYPE.SENDSCORENETWORK, null, goodCards);
        }
        gameManager.eventManager.PostNotification(EVENT_TYPE.SENDSCORE, null, goodCards);
        gameManager.score = goodCards;
    }
}
