using UnityEngine;
using System.Collections.Generic;

public class ResultChecker:MonoBehaviour {
    public MainManager GameManager;  
    private bool ShowingResults = false;
    private List<Card> GoodCards;
    private List<Card> BadCards;

	void Start()
    {
        GoodCards = new List<Card>();
        BadCards = new List<Card>();
    }
    
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P) && !ShowingResults)
        {          
            ShowingResults = true;
            CalculateResults();
        }
        else if(Input.GetKeyDown(KeyCode.P) && ShowingResults)
        {
            ShowingResults = false;
            HideResults();
        }
        */
    }

    public int CalculateResults()
    {
        GoodCards.Clear();
        BadCards.Clear();
        //Retreive all subtopicmatchers in the placed cards parents and check if their codes match
        foreach (KeyValuePair<string, Card> card in GameManager.CardController.PlacedCards)//LOD violation
        {
            SubtopicMatcher matcher = card.Value.GetComponentInParent<SubtopicMatcher>();
            string matcherCode = matcher.MatchCode.Substring(0, 2);
            string cardCode = card.Value.MatchCode.Substring(0, 2);
            if (matcher && matcherCode == cardCode)
            {
                GoodCards.Add(card.Value);                
            }
            else
            {
                BadCards.Add(card.Value);
                
            }           
        }
        ShowResults();
        return GoodCards.Count;
    }

    private void ShowResults()
    {
        foreach(Card card in GoodCards)
        {
            card.GetComponent<Renderer>().material.color = Color.green;
            card.GetComponentInChildren<SpriteRenderer>().material.color = Color.green;
            GameObject go = Object.Instantiate(GameManager.GoodPlacementEffect);
            go.transform.position = card.transform.position;
        }

        foreach(Card card in BadCards)
        {
            card.GetComponent<Renderer>().material.color = Color.red;
            card.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
            GameObject go = Object.Instantiate(GameManager.WrongPlacementEffect);
            go.transform.position = card.transform.position;
        }
    }

    public void HideResults()
    {
        GoodCards.Clear();
        BadCards.Clear();
        //Retreive all subtopicmatchers in the placed cards parents and check if their codes match
        foreach (KeyValuePair<string, Card> card in GameManager.CardController.PlacedCards)//LOD violation
        {
            card.Value.GetComponent<Renderer>().material.color = Color.white;
            card.Value.GetComponentInChildren<SpriteRenderer>().material.color = Color.white;         
        }
    }
}
