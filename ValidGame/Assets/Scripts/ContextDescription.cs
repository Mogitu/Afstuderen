﻿using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Add to gameobjects that need an attached textmesh displayed in the (context)infobar on top of the gamescene. 
/// </summary>
public class ContextDescription : MonoBehaviour
{
    private TextMesh DescriptionTxtMesh;     
    void Awake()
    {
        DescriptionTxtMesh = GetComponentInChildren<TextMesh>();
      
        if (gameObject.tag=="ValidTopic")
        {
            GameObject go = Instantiate(Resources.Load("coordinateSubtopic")) as GameObject;
            go.transform.parent = transform;
            //TODO: this only sets local pos during running the game, it needs be "ok" in scene to.
            go.transform.localPosition = new Vector3(-0.0197f, -0.00867f, 0.00249f);
            go.transform.localScale = new Vector3(0.0158814f, 0.02788523f, 0.003244675f);

        }
        else
        {
            GameObject go = Instantiate(Resources.Load("coordinateSubtopic")) as GameObject;
            go.transform.parent = transform;
            //TODO: this only sets local pos during running the game, it needs be "ok" in scene to.
            go.transform.localPosition = new Vector3(-0.02006f, -0.00245f, -0.00073f);
        }        
    }

    public string DescriptionText{
        get { return DescriptionTxtMesh.text; }
    }
}

