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

    public List<GameObject> browsableCards = new List<GameObject>();

    void Start()
    {
        int offSetX = -400;
        for (int i = 0; i < browsableCards.Count; i++)
        {
            GameObject obj = Instantiate(browsableCards[i]);
            obj.transform.SetParent(cardPanel.transform);
            Vector3 newPos = obj.transform.parent.transform.position;
            newPos.x += offSetX;
            offSetX += 100;
            obj.transform.position = newPos;

            Button objBtn = obj.GetComponent<Button>();
            objBtn.onClick.AddListener(() => { ClickedCard(); objBtn.gameObject.SetActive(false); });
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
        }
        else
        {
            cardPanel.SetActive(true);
        }
    }

    public void ClickedCard()
    {
        ShowCards();
        GameManager.Instance.SelectCard();
    }
}
