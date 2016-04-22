using UnityEngine;
using AMC.Camera;
using System.Collections.Generic;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   ...
/// </summary>
public class GameOverState : GameState
{

    private int GoodCards = 0;

    public GameOverState(EventManager manager)
            : base(manager)
    {
        EventManager.AddListener(GameEvents.EndMultiplayer, OnEndMultiplayer);
        EventManager.AddListener(GameEvents.EndPractice, OnEndPractice);
    }

    private void OnEndMultiplayer(short gameEvent, Component sender, object obj)
    {
        OnEndPractice(gameEvent, sender, obj);
        EventManager.PostNotification(GameEvents.SendScoreNetwork, null, GoodCards);
    }

    private void OnEndPractice(short gameEvent, Component sender, object obj)
    {
        DetermineResults();
        Camera.main.GetComponent<CameraController>().enabled = false;
        Camera.main.GetComponent<Animator>().enabled = true;
        Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
        DisableAllColliders();
        EventManager.PostNotification(GameEvents.SendScore, null, GoodCards);
    }

    public override void UpdateState()
    {

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
        ResultChecker checker = Object.FindObjectOfType<ResultChecker>();
        GoodCards = checker.CalculateResults();
    }
}
