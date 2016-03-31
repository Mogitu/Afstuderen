using UnityEngine;
using AMC.Camera;
using System.Collections.Generic;
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
            Camera.main.GetComponent<CameraController>().enabled = false;
            Camera.main.GetComponent<Animator>().enabled = true;
            Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
            DisableAllColliders();
            FirstRun = true;
        }
    }

    /// <summary>
    /// Disables colliders on all objects in the scene
    /// </summary>
    private void DisableAllColliders()
    {
        BoxCollider[] objects = Object.FindObjectsOfType<BoxCollider>();
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].enabled = false;
        }
    }

    /// <summary>
    /// Check how many cards are properly placed.
    /// TODO    :   Violates law of demeter!
    /// </summary>
    public void DetermineResults()
    {
        //Retreive all subtopicmatchers in the placed cards parents and check if their codes match
        foreach (KeyValuePair<string, Card> card in GameManager.CardController.PlacedCards)//LOD violation
        {
            SubtopicMatcher matcher = card.Value.GetComponentInParent<SubtopicMatcher>();
            string matcherCode = matcher.MatchCode.Substring(0, 2);
            string cardCode = card.Value.MatchCode.Substring(0, 2);
            if (matcher && matcherCode == cardCode)
            {
                card.Value.GetComponent<Renderer>().material.color = Color.green;
                card.Value.GetComponentInChildren<SpriteRenderer>().material.color = Color.green;
                GameObject go = Object.Instantiate(GameManager.GoodPlacementEffect);
                go.transform.position = card.Value.transform.position;
                GoodCards++;
            }
            else
            {
                card.Value.GetComponent<Renderer>().material.color = Color.red;
                card.Value.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                GameObject go = Object.Instantiate(GameManager.WrongPlacementEffect);
                go.transform.position = card.Value.transform.position;
            }
        }
        SendScores();
    }

    public void SendScores()
    {
        //If it is an multiplayer game the score needs to be sent to the opposing player.
        if (GameManager.IsMultiplayerGame)
        {
            GameManager.EventManager.PostNotification(GameEvents.SendScoreNetwork, null, GoodCards);
        }
        GameManager.EventManager.PostNotification(GameEvents.SendScore, null, GoodCards);
        GameManager.Score = GoodCards;
    }
}
