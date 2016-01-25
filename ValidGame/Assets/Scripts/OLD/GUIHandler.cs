﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        public GameObject gameOverPanel;
        public GameObject cardPanelContent;
        public Text resultText;
        public GameObject extraCardInfoPanel;
        public GameObject infoBar;

        private List<GuiCard> browsableCards = new List<GuiCard>();

        void Start()
        {
            PopulateCardBrowser();
            infoBar.SetActive(false);
        }

        private void PopulateCardBrowser()
        {
            GuiCard[] cards = FindObjectsOfType<GuiCard>();
            int offSetX = -225;
            int offSetY = 200;
            int col = 1;
            for (int i = 0; i < cards.Length; i++)
            {
                GuiCard obj = cards[i];
                obj.transform.SetParent(cardPanelContent.transform,false);
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

        public void Update()
        {
            if (GameManager.Instance.gameState == GameManager.Instance.gameOverState)
            {
                gameOverPanel.SetActive(true);
                gameMenu.SetActive(false);
                infoBar.SetActive(false);
                cardButton.SetActive(false);
                resultText.text = GameManager.Instance.scoreText;            
            }
        }

        public void ClickStartSingle()
        {
            mainMenu.SetActive(false);
            gameMenu.SetActive(false);
            cardButton.SetActive(true);
            infoBar.SetActive(true);
            GameManager.Instance.StartPractice();
        }


        public void ShowCards()
        {            
            if (cardPanel.activeSelf)
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.ENABLE, this);
                cardPanel.SetActive(false);               
                GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard, true);
            }
            else
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.DISABLE, this);
                cardPanel.SetActive(true);               
                GameManager.Instance.DisableEnableScripts(GameManager.Instance.gameBoard, false);
            }
        }

        public void ClickedCard(GameObject obj)
        {
            ShowCards();
            GuiCard card = obj.GetComponent<GuiCard>();
            GameManager.Instance.SelectCard(card.matchCode);
            obj.gameObject.SetActive(false);
        }

        public void ClickExit()
        {            
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }

