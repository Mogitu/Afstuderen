using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour {

    public GameObject gameMenu;
    public GameObject mainMenu;
    public GameObject mainSingleMenu;
    public GameObject mainMultiMenu;
    public GameObject cardButton;
    public GameObject cardPanel;

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
   
}
