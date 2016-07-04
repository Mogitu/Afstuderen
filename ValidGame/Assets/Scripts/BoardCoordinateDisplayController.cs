using System;
using UnityEngine;


public class BoardCoordinateDisplayController : MonoBehaviour
{
    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;
    public GameObject DisplayObject;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            HandleContextInfo(hit);
        }
    }

    private void HandleContextInfo(RaycastHit hit)
    {
        if (hit.transform.gameObject.name == "subtopicinfo")
        {
            DisplayObject.SetActive(true);
            DisplayObject.transform.position = hit.transform.TransformPoint(OffsetX, OffsetZ, OffsetY); //new Vector3(newX, DisplayObject.transform.position.y, newZ);           
        }
        else
        {
            DisplayObject.SetActive(false);
        }
    }
}

