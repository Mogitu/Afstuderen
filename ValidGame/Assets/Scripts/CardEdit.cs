using UnityEngine;
using System.Collections;

public class CardEdit : MonoBehaviour {
    public string matchCode;
    public string title;
    public string description;

    public SpriteRenderer sprite;
    public TextMesh txtMeshTitle;
    public TextMesh txtMeshDesc;   

	// Use this for initialization
	void Start () {       
        txtMeshTitle.text = title;
        txtMeshDesc.text = description;    
	}

    public void SetData(string title, string description, string matchCode)
    {
        this.title = title;
        this.description = description;
        txtMeshTitle.text = title;
        txtMeshDesc.text = description;
        this.matchCode = matchCode;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
