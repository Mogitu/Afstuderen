using UnityEngine;
using System.Collections;
using System;

public class CoordinateScript : MonoBehaviour
{
    private int ShowCoordinate;
    private EventManager EventManager;
    // Use this for initialization
    void Start()
    {
        EventManager = FindObjectOfType<EventManager>();
        EventManager.AddListener(GameEvents.UpdateSettings, OnUpdateSettings);
        ShowCoordinate = PlayerPrefs.GetInt("ShowCoordinates", 0);
        SetCoordinate();
        if (ShowCoordinate == 1)
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
       
    }

    private void OnUpdateSettings(short eventType, Component sender, object param)
    {
        ShowCoordinate = PlayerPrefs.GetInt("ShowCoordinates", 0);
        SetCoordinate();
        if (ShowCoordinate == 1)
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void HideCoordinate()
    {
        gameObject.SetActive(false);
    }


    private void DisplayCoordinate()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Displays coordinates of the topics on the gameboard
    /// </summary>
    private void SetCoordinate()
    {
        TextMesh txtMesh = GetComponent<TextMesh>();
        GameObject root = transform.parent.parent.gameObject;
        SubtopicMatcher match = root.GetComponentInChildren<SubtopicMatcher>();

        if (match != null)
        {
           
            string txt = match.MatchCode;
            txtMesh.text = txt.Insert(1," ");
        }
        else
        {
            
            string txt =  transform.parent.name.Substring(transform.parent.name.Length-1,1);
            txtMesh.text = String.Format("{0:# #}", txt);
        }
    }
}
