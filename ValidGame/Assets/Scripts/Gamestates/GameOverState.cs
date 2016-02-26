using UnityEngine;
using AMC.Camera;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   ...
/// </summary>
public class GameOverState : GameState
{
    private bool FirstRun = false;
    private int GoodCards = 0;

    public GameOverState(MainManager manager)
            : base(manager)
    {
    }

    public override void UpdateState()
    {
        //Only run this once
        if (!FirstRun)
        {
            DetermineResults();
            FirstRun = true;
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
        for (int i = 0; i < GameManager.CardManager.PlacedCards.Count; i++)//LOD violation
        {
            SubtopicMatcher matcher = GameManager.CardManager.PlacedCards[i].GetComponentInParent<SubtopicMatcher>();
            if (matcher && matcher.MatchCode == GameManager.CardManager.PlacedCards[i].MatchCode)
            {                
                GameManager.CardManager.PlacedCards[i].GetComponent<Renderer>().material.color = Color.green;
                GoodCards++;
            }
            else
            {              
                GameManager.CardManager.PlacedCards[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
        Debug.Log(GoodCards + " properly placed cards.");
        SendScores();
    }

    public void SendScores()
    {
        if (GameManager.IsMultiplayerGame)
        {
            GameManager.EventManager.PostNotification(GameEvents.SendScoreNetwork, null, GoodCards);
        }
        GameManager.EventManager.PostNotification(GameEvents.SendScore, null, GoodCards);
        GameManager.Score = GoodCards;
    }
}
