using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Contains all data used to populate the card front with.
///             The matchcode is used to compare a card against the subtopic it is placed on.
/// TODO    :   Currently used by both the ingame cards and the cardbuilder, the card builder needs to be 
///             Separated from this class by since it should not use scripts outside the editor folder.
///             A flyweight object can be a possible solutions.  
/// </summary>
public class Card : MonoBehaviour
{
    public string MatchCode;
    //public string Title;
    public TeamType TypeOfCard;
    //public string Description;
    public SpriteRenderer Sprite;
    //public TextMesh TxtMeshTitle;
    //public TextMesh TxtMeshDesc;

    // Use this for initialization
    void Start()
    {
        //TxtMeshTitle.text = Title;
        //TxtMeshDesc.text = Description;
    }

    //Set needed properties
    public void SetData(string matchCode, TeamType cardType)
    {
       //Title = title;
       //Description = description;
       //TxtMeshTitle.text = title;
       //TxtMeshDesc.text = description;
       MatchCode = matchCode;
       TypeOfCard = cardType;
    }
}