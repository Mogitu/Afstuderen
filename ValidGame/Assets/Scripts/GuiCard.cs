using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

namespace AMCTools
{
    public class GuiCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public string matchCode = "A01";

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            GUIHandler handler = GetComponentInParent<GUIHandler>();
            GameObject info = handler.extraCardInfoPanel;
            Image[] img = info.GetComponentsInChildren<Image>();
            if (img[1])
            {
                img[1].enabled = true;
                img[1].sprite = GetComponent<Image>().sprite;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GUIHandler handler = GetComponentInParent<GUIHandler>();
            GameObject info = handler.extraCardInfoPanel;
            Image[] img = info.GetComponentsInChildren<Image>();
            if (img[1])
            {
                img[1].enabled = false;
            }
        }
    }

}
