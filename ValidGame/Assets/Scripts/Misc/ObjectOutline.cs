using UnityEngine;
using System.Collections;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Changes color of object when mouse hovers on it.
/// TODO    :   Rather than OnMouseX, a raycast would be more stable on different platforms.
/// </summary>

public class ObjectOutline : MonoBehaviour
{
    // Use this for initialization  
    public Color hoverColor;
    private Renderer objectRenderer;
    private Material defaultMat;
    private Material highLightMat;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        highLightMat = (Material)Instantiate(objectRenderer.sharedMaterial);
        defaultMat = objectRenderer.sharedMaterial;
    }

    void OnMouseEnter()
    {
        if (enabled)
        {
            objectRenderer.material = highLightMat;
            highLightMat.color = hoverColor;
        }
    }

    void OnMouseExit()
    {
        if (enabled)
            objectRenderer.material = defaultMat;
    }
}

