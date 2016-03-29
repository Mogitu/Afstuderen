using UnityEngine;
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

    private Shader DefaultShader;
    private Shader OutlineShader;

    void Start()
    {
        DefaultShader = Shader.Find("Standard");
        OutlineShader = Shader.Find("Toon/Basic Outline");
        ObjectRenderer = GetComponent<Renderer>();
        HighLightMat = Instantiate(ObjectRenderer.sharedMaterial);//Shared material because normal material will create extra instances "killing" performance.
        DefaultMat = ObjectRenderer.sharedMaterial;
    }

    void OnMouseEnter()
    {
        if (enabled)
        {
            //ObjectRenderer.material = HighLightMat;
            //HighLightMat.color = HoverColor;
            ObjectRenderer.material.shader = OutlineShader;
        }
    }

    void OnMouseExit()
    {
        if (enabled)
            // ObjectRenderer.material = DefaultMat;
            ObjectRenderer.material.shader = DefaultShader;
    }
}

