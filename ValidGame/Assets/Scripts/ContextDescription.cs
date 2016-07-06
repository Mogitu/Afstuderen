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
        GameObject go = Instantiate(Resources.Load("coordinateSubtopic")) as GameObject;
        go.transform.parent = transform;
        if (gameObject.tag=="ValidTopic")
        {            
            //TODO: this only sets local pos during running the game, it needs be "ok" in scene to.
            go.transform.localPosition = new Vector3(-0.0197f, -0.004f, 0.0129f);
            go.transform.localScale = new Vector3(0.0158814f, 0.02788523f, 0.003244675f)/1.5f;
        }
        else
        {          
            //TODO: this only sets local pos during running the game, it needs be "ok" in scene to.
            go.transform.localPosition = new Vector3(-0.02006f, -0.00245f, -0.00073f);
        }        
    }

    public string DescriptionText{
        get { return DescriptionTxtMesh.text; }
    }
}

