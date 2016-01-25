using UnityEngine;
using System.Collections;


    //----------------------------------------------------------------------------------
    // Class    : SubtopicMatcher
    // Desc     : Attached to subtopic card holders to compare the placed cards with their matchcode,
    //            Also tracks whether the slot is currently occupied or not.            
    // ----------------------------------------------------------------------------------
    public class SubtopicMatcher : MonoBehaviour
    {
        // Use this for initialization
        public string matchCode;
        public bool occupied = false;      
    }


