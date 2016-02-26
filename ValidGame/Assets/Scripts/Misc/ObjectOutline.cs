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
    public Color HoverColor;
    private Renderer ObjectRenderer;
    private Material DefaultMat;
    private Material HighLightMat;

    void Start()
    {
        ObjectRenderer = GetComponent<Renderer>();
        HighLightMat = (Material)Instantiate(ObjectRenderer.sharedMaterial);//Shared material because normal material will create extra instances "killing" performance.
        DefaultMat = ObjectRenderer.sharedMaterial;
    }

    void OnMouseEnter()
    {
        if (enabled)
        {
            ObjectRenderer.material = HighLightMat;
            HighLightMat.color = HoverColor;
        }
    }

    void OnMouseExit()
    {
        if (enabled)
            ObjectRenderer.material = DefaultMat;
    }
}

