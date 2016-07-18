using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using AMC.GUI;
using System.Collections;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
public class CardbrowserView : View
{
    private Dictionary<string, GuiCard> BrowsableCards; 
    public GameObject CardPanelContent;
    public Image ExtraInfoPanelImage;
    private GuiPresenter GuiPresenter;
    private float OffsetX = 80;
    private float OffsetY = -90;

    private float TargetResX = 1024;
    private float TargetResY = 768;
    private float CurrentResX ;
    private float CurrentResY;

    private float ResOffsetX;
    private float ResOffsetY;

    void Awake()
    {
        CalculateOffsets();
        GuiPresenter = GetPresenterType<GuiPresenter>();
        BrowsableCards = new Dictionary<string, GuiCard>();
    }

    private void CalculateOffsets()
    {
        CurrentResX = Screen.width;
        CurrentResY = Screen.height;
        ResOffsetX = CurrentResX / TargetResX;
        ResOffsetY = CurrentResY / TargetResY;      
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateOffsets();

    }

    void Start()
    {
        PopulateContent();
        IEnumerator enumerator = BrowsableCards.Keys.GetEnumerator();
        enumerator.MoveNext();
        string first = enumerator.Current.ToString();
        ExtraInfoPanelImage.sprite = BrowsableCards[first].GetComponent<Image>().sprite;
    }

    /// <summary>
    /// Attach all cards to the content browser.
    /// TODO: get rid of hardcoded offsets. Also, perhaps, this is to much logic for in the view?
    /// </summary>
    private void PopulateContent()
    {
        //float widthScale = 1024 / Screen.width;
        //float heightScale = 768 / Screen.height;
        GuiCard[] cards = FindObjectsOfType<GuiCard>();
        float offSetX = OffsetX;
        float offSetY = OffsetY;
        int col = 1;     
        //add all cards
        if (GuiPresenter.GetTeamType == TeamType.ALL)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                //AddCard(cards[i], ref offSetX, ref offSetY, ref col);
                AddCard(cards[i], ref offSetX, ref offSetY, ref col);
            }
        }
        //only add cards of selected teamtype
        else
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (GuiPresenter.GetTeamType == cards[i].TeamType)
                {
                    AddCard(cards[i], ref offSetX, ref offSetY, ref col);
                }
            }
        }

        //TODO: SLOPPY CALL; essentially populates the view twice.
        RepopulateContent();
    }

    private void RepopulateContent()
    {
        float offSetX = OffsetX;
        float offSetY = OffsetY;
        int col = 1;
        foreach (KeyValuePair<string, GuiCard> card in BrowsableCards)
        {
            GuiCard guiCard = card.Value;
            guiCard.transform.SetParent(CardPanelContent.transform, false);
            Vector3 newPos = guiCard.transform.parent.transform.position;
            newPos.x += offSetX;
            newPos.y += offSetY;
            offSetX += (guiCard.GetComponent<RectTransform>().rect.width*1.1f)*ResOffsetX;
            guiCard.transform.position = newPos;
            col++;
            if (col >= 5)
            {
                col = 1;
                offSetY -= 200;
                offSetX = OffsetX;
            }
        }
    }

    //TODO: Split this up into smaller methods and get rid of hardcoded items
    private void AddCard(GuiCard card, ref float offSetX, ref float offSetY, ref int col)
    {
        GuiCard guiCard = card;
        guiCard.transform.SetParent(CardPanelContent.transform, false);
        Vector3 newPos = guiCard.transform.parent.transform.position;
        //newPos.x += offSetX*ResOffsetX;
        //newPos.y += offSetY*ResOffsetY;
        //offSetX += 150;
      //  guiCard.transform.position = newPos;
        Button objBtn = guiCard.GetComponent<Button>();
        objBtn.onClick.AddListener(() => { ClickedCard(objBtn.gameObject); });

        if (BrowsableCards.ContainsKey(guiCard.MatchCode))
        {
            guiCard.MatchCode = guiCard.MatchCode + "2";
            guiCard.name = "GuiCard" + guiCard.MatchCode;
            BrowsableCards.Add(guiCard.MatchCode, guiCard);
        }
        else
        {
            BrowsableCards.Add(guiCard.MatchCode, guiCard);
        }
        col++;

        if (col >= 5)
        {
          //  col = 1;
          //  offSetY -= 200;
           // offSetX = OffsetX;
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
                GuiCard card = result.gameObject.GetComponent<GuiCard>();
                if (card != null)
                {
                    Image img = card.GetComponent<Image>();
                    ExtraInfoPanelImage.sprite = img.sprite;
                    return;
                }
            }
        }
    }

    //TODO  :   Ties card model to this view, defeating the purpose of mvp?
    public void ClickedCard(GameObject obj)
    {
        GuiCard card = obj.GetComponent<GuiCard>();        
        GetPresenterType<GuiPresenter>().PickCard(card.MatchCode);
        BrowsableCards.Remove(card.MatchCode);
        obj.SetActive(false);
        RepopulateContent();
        GuiPresenter.EventManager.PostNotification(GameEvents.RestartTimer, this);
    }
}
