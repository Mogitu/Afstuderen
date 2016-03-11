using UnityEngine;
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
    }

    public string DescriptionText{
        get { return DescriptionTxtMesh.text; }
    }
}

