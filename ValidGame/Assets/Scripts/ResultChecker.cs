using UnityEngine;
using System.Collections.Generic;

public class ResultChecker:MonoBehaviour {

    public MainManager GameManager;
    private bool ShowingResults = false;

	void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !ShowingResults)
        {
          
            ShowingResults = true;
            ShowResults();
        }
        else if(Input.GetKeyDown(KeyCode.P) && ShowingResults)
        {
            ShowingResults = false;
            HideResults();
        }
    }

    private void ShowResults()
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
                //GoodCards++;
            }
            else
            {
                card.Value.GetComponent<Renderer>().material.color = Color.red;
                card.Value.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                GameObject go = Object.Instantiate(GameManager.WrongPlacementEffect);
                go.transform.position = card.Value.transform.position;
            }
        }
    }

    private void HideResults()
    {
        //Retreive all subtopicmatchers in the placed cards parents and check if their codes match
        foreach (KeyValuePair<string, Card> card in GameManager.CardController.PlacedCards)//LOD violation
        {
            card.Value.GetComponent<Renderer>().material.color = Color.white;
            card.Value.GetComponentInChildren<SpriteRenderer>().material.color = Color.white;         
        }
    }
}
