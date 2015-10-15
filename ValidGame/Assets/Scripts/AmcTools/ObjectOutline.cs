using UnityEngine;
using System.Collections;

public class ObjectOutline : MonoBehaviour {

    // Use this for initialization  
   public Color hoverColor;
   private Color normalColor;
   private Renderer objectRenderer;

	void Start () {
        objectRenderer = GetComponent<Renderer>();
        normalColor = objectRenderer.material.color;
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
