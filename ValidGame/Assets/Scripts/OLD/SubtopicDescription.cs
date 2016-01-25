using UnityEngine;
using UnityEngine.UI;
using System.Collections;

    public class SubtopicDescription : MonoBehaviour
    {
        public TextMesh descriptionTxt;
        private GameObject infoObject;
        // Use this for initialization

        void Awake()
        {
            infoObject = GameObject.Find("Infobar");
        }
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseEnter()
        {
            Text infoText = infoObject.GetComponentInChildren<Text>();
            if (infoText)
                infoText.text = descriptionTxt.text;
        }


        void OnMouseExit()
        {
            Text infoText = infoObject.GetComponentInChildren<Text>();
            if (infoText)
                infoText.text = "";
        }
    }

