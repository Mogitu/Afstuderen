using UnityEngine;
using System.Collections;

public class ObjectOutline : MonoBehaviour {

    // Use this for initialization
    Renderer objectRenderer;
   public Color normalColor;
   public Color hoverColor;
	void Start () {
        objectRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        objectRenderer.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        objectRenderer.material.color = normalColor;
    }
}
