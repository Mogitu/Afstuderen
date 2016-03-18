using UnityEngine;
using System.Collections;

public class CardModel  {

    public string Title;
    public string MatchCode;    
    //public TeamType TypeOfCard;
    public string Description;

    public CardModel(string title, string matchCode, string description)
    {
        Title = title;
        MatchCode = matchCode;
        Description = description;
    }
}
