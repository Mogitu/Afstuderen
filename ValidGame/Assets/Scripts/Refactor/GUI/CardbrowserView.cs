using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CardbrowserView : View
{
    private List<GuiCard> browsableCards;
    public GameObject cardPanelContent;
    public Image extraInfoPanelImage;

    void Awake()
    {
        browsableCards = new List<GuiCard>();
    }

    void Start()
    {
        PopulateContent();
    }

    /// <summary>
    /// Attach all cards to the content browser.
    /// TODO: get rid of hardcoded offsets.
    /// </summary>
    private void PopulateContent()
    {
        GuiCard[] cards = FindObjectsOfType<GuiCard>();
        int offSetX = -225;
        int offSetY = 200;
        int col = 1;
        for (int i = 0; i < cards.Length; i++)
        {
            GuiCard obj = cards[i];
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
            foreach (RaycastResult r in hits)
            {
                GuiCard card = r.gameObject.GetComponent<GuiCard>();
                Image img = card.GetComponent<Image>();
                if (card != null && img.sprite != null)
                {
                    extraInfoPanelImage.sprite = img.sprite;
                    return;
                }
            }
        }
    }

    public void ClickedCard(GameObject obj)
    {
        /*
        ShowCards();
        GuiCard card = obj.GetComponent<GuiCard>();
        GameManager.Instance.SelectCard(card.matchCode);
        obj.gameObject.SetActive(false);
        */
    }
}
