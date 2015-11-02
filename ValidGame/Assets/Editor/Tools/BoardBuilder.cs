using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AMCTools
{
    public class BoardBuilder : EditorWindow
    {
        [MenuItem("AMC Centre/Tools/VALID/Board builder")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(BoardBuilder),false, "Board builder");            
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

