﻿using UnityEngine;
using System.Collections;


    //----------------------------------------------------------------------------------
    // Class    : Card
    // Desc     : Contains all data used to populate the card front with.
    //            The matchcode is used to compare a card against the subtopic it is placed on.
    // TODO     : Currently used by both the ingame cards and the cardbuilder, the card builder needs to be 
    //            Separated from this class by since it should not use scripts outside the editor folder.
    //            A flyweight/adapter/proxy object can be possible solutions.  
    // -----------------
    public class Card : MonoBehaviour
    {
        public string matchCode;
        public string title;
        public string description;

        public SpriteRenderer sprite;
        public TextMesh txtMeshTitle;
        public TextMesh txtMeshDesc;  

        // Use this for initialization
        void Start()
        {
            txtMeshTitle.text = title;
            txtMeshDesc.text = description;
           
            EventManager.Instance.AddListener(EVENT_TYPE.DISABLE, DisableCollider);
            EventManager.Instance.AddListener(EVENT_TYPE.ENABLE, EnableCollider);
        }

        void DisableCollider(EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            GetComponent<Collider>().enabled = false;                
        }

        void EnableCollider(EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            GetComponent<Collider>().enabled = true;        
        }
          
        //Set needed properties
        public void SetData(string title, string description, string matchCode)
        {
            this.title = title;
            this.description = description;
            txtMeshTitle.text = title;
            txtMeshDesc.text = description;
            this.matchCode = matchCode;
        }      
    }

