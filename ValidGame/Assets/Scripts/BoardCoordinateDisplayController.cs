using System;
using UnityEngine;


public class BoardCoordinateDisplayController : MonoBehaviour
{
    public EventManager EventManager;
    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;
    public GameObject DisplayObject;
    private int ShowDisplayObject;

    void Start()
    {
        EventManager.AddListener(GameEvents.UpdateSettings, OnUpdateSettings);
        DisplayObject.SetActive(false);
    }

    private void OnUpdateSettings(short eventType, Component sender, object param)
    {
        ShowDisplayObject = PlayerPrefs.GetInt("ShowCoordinates");
    }

    void Update()
    {
        if (ShowDisplayObject==1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                HandleContextInfo(hit);
            }
        }
        
    }

    private void HandleContextInfo(RaycastHit hit)
    {
        if (hit.transform.gameObject.name == "subtopicinfo")
        {
            DisplayObject.SetActive(true);
            DisplayObject.transform.position = hit.transform.TransformPoint(OffsetX, OffsetZ, OffsetY);         
        }
        else
        {
            DisplayObject.SetActive(false);
        }
    }
}

