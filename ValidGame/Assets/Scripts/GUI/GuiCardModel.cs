using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuiCardModel : MonoBehaviour, IModel /*IPointerEnterHandler, IPointerExitHandler*/
{
    public string matchCode = "1a";
    public GuiPresenter presenter;

    public void SetPresenter(GuiPresenter presenter)
    {
        this.presenter = presenter;
    }

    /*
    //TODO: part of old system, remove after refactor.
    public void OnPointerEnter(PointerEventData eventData)
    {
        GUIHandler handler = GetComponentInParent<GUIHandler>();
        if(handler != null) 
        {
            GameObject info = handler.extraCardInfoPanel;
            Image[] img = info.GetComponentsInChildren<Image>();
            if (img[1])
            {
                img[1].enabled = true;
                img[1].sprite = GetComponent<Image>().sprite;
            }
        }      
    }

    //TODO: part of old system, remove after refactor.
    public void OnPointerExit(PointerEventData eventData)
    {
        GUIHandler handler = GetComponentInParent<GUIHandler>();
        if(handler != null)
        {
            GameObject info = handler.extraCardInfoPanel;
            Image[] img = info.GetComponentsInChildren<Image>();
            if (img[1])
            {
                img[1].enabled = false;
            }
        }       
    }
    */
}

