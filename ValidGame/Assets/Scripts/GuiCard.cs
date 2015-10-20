using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class GuiCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string matchCode = "A01";
    public float maxScale = 2.0f;
    private Vector2 originalPosition;
    private Vector2 originalScale;
	// Use this for initialization
	void Start () {
        originalScale= transform.localScale;
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * maxScale;
        //transform.position = new Vector2(0.5f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        //transform.position = originalPosition;
    }
}
