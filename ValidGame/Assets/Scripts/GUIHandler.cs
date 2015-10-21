using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUIHandler : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject mainMenu;
    public GameObject mainSingleMenu;
    public GameObject mainMultiMenu;
    public GameObject cardButton;
    public GameObject cardPanel;

    private List<GuiCard> browsableCards = new List<GuiCard>();

    void Start()
    {
        PopulateCardBrowser();
    }

    private void PopulateCardBrowser()
    {
        GuiCard[] cards = Resources.LoadAll<GuiCard>("Gamecards/GUI");
        int offSetX = -245;
        for (int i = 0; i < cards.Length; i++)
        {
            GuiCard obj = Instantiate(cards[i]);
            obj.transform.SetParent(cardPanel.transform);
            Vector3 newPos = obj.transform.parent.transform.position;
            newPos.x += offSetX;
            offSetX += 100;
            obj.transform.position = newPos;
            Button objBtn = obj.GetComponent<Button>();
            objBtn.onClick.AddListener(() => { ClickedCard(objBtn.gameObject);});          
            browsableCards.Add(obj);
        }
    }

    public void ClickStartSingle()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        cardButton.SetActive(true);
        GameManager.Instance.StartPractice();
    }

    public void ClickMultiPlayer()
    {
        mainMenu.SetActive(false);
        mainSingleMenu.SetActive(false);
        mainMultiMenu.SetActive(true);
    }

    public void ClickHost()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        mainMultiMenu.SetActive(false);
    }

    public void ClickJoin()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        mainMultiMenu.SetActive(false);
        GameManager.Instance.JoinGame();
    }

    public void ShowCards()
    {
        if (cardPanel.activeSelf)
        {
            cardPanel.SetActive(false);
            GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard, true);
            GameManager.Instance.gameState = GameManager.GAMESTATE.PLACING;
        }
        else
        {
            cardPanel.SetActive(true);
            GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard,false);
            GameManager.Instance.gameState = GameManager.GAMESTATE.BROWSING;
        }
    }

    public void ClickedCard(GameObject obj)
    {
        ShowCards();
        GuiCard card = obj.GetComponent<GuiCard>();
        GameManager.Instance.SelectCard(card.matchCode);
        obj.gameObject.SetActive(false); 
    }
}
