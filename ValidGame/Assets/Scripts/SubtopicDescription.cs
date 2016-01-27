using UnityEngine;
using UnityEngine.UI;

public class SubtopicDescription : MonoBehaviour
{
    public TextMesh descriptionTxt;
    //private GameObject infoObject;
    // Use this for initialization

    void Awake()
    {
       // infoObject = GameObject.Find("Infobar");
    }

    /*
    /// <summary>
    /// TODO: OLD system, remove after refactor
    /// </summary>
    void OnMouseEnter()
    {
        Text infoText = infoObject.GetComponentInChildren<Text>();
        if (infoText)
            infoText.text = descriptionTxt.text;
    }


    /// <summary>
    /// TODO: OLD system, remove after refactor
    /// </summary>
    void OnMouseExit()
    {
        Text infoText = infoObject.GetComponentInChildren<Text>();
        if (infoText)
            infoText.text = "";
    }
    */
}

