using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
public class CardbrowserView : View
{
    private List<GuiCardModel> browsableCards;
    public GameObject cardPanelContent;
    public Image extraInfoPanelImage;

    void Awake()
    {
        browsableCards = new List<GuiCardModel>();
    }

    void Start()
    {
        PopulateContent();
    }

    /// <summary>
    /// Attach all cards to the content browser.
    /// TODO: get rid of hardcoded offsets. Also, perhaps, this is to much logic for in the view?
    /// </summary>
    private void PopulateContent()
    {
        GuiCardModel[] cards = FindObjectsOfType<GuiCardModel>();
        int offSetX = -225;
        int offSetY = 200;
        int col = 1;
        for (int i = 0; i < cards.Length; i++)
        {
            GuiCardModel obj = cards[i];            
            obj.transform.SetParent(cardPanelContent.transform, false);
            Vector3 newPos = obj.transform.parent.transform.position;
            newPos.x += offSetX;
            newPos.y += offSetY;
            offSetX += 150;
            obj.transform.position = newPos;
            Button objBtn = obj.GetComponent<Button>();
            objBtn.onClick.AddListener(() => { ClickedCard(objBtn.gameObject); });
            browsableCards.Add(obj);
            col++;

            if (col >= 5)
            {
                col = 1;
                offSetY -= 200;
                offSetX = -225;
            }
        }
    }

    void Update()
    {
        UpdateInfoPanel();
    }

    /// <summary>
    /// Display the image of the current highlighted card on the infopanel
    /// TODO    :   Maybe to much logic, decouple?
    /// </summary>
    private void UpdateInfoPanel()
    {
        // get pointer event data, then set current mouse position
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;

        // create an empty list of raycast results
        List<RaycastResult> hits = new List<RaycastResult>();

        // ray cast into UI and check for hits
        EventSystem.current.RaycastAll(ped, hits);

        if (hits.Count > 0)
        {
            // check any hits to see if any of them are blocking UI elements
            foreach (RaycastResult result in hits)
            {
               GuiCardModel card = result.gameObject.GetComponent<GuiCardModel>();                              
               if(card != null)
                {
                    Image img = card.GetComponent<Image>();
                    extraInfoPanelImage.sprite = img.sprite;
                    return;
                }              
            }
        }
    }

    //TODO  :   Ties card model to this view, defeating the purpose of mvp?
    public void ClickedCard(GameObject obj)
    {
        GuiCardModel card = obj.GetComponent<GuiCardModel>();        
        presenter.PickCard(card.matchCode);
        obj.SetActive(false);   
    }
}
