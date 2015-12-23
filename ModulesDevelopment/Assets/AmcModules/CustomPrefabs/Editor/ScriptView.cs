using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace AmcCustomPrefab
{
    class ScriptView : IView
    {
        private static string scriptToParse = "";
        private bool validScript = false;
        private bool error = false;
        private AmcCustomPrefab myPrefab;

        public void Display()
        {
            bool wrap = EditorStyles.textField.wordWrap;
            EditorStyles.textField.wordWrap = true;
            //Create a text area that fills the window
            string updatedScript = EditorGUILayout.TextArea(scriptToParse, GUILayout.ExpandHeight(true));
            EditorStyles.textField.wordWrap = wrap;

            if (!updatedScript.Equals(scriptToParse))
            {
                //invalidate our prefab and script when the script changes
                validScript = false;
                error = false;
            }
            scriptToParse = updatedScript;

            if (GUILayout.Button("Parse script"))
            {
                //Pass in the contents of the textarea as the script for this prefab
                myPrefab = new AmcCustomPrefab("TestPrefab", scriptToParse);
                if (myPrefab.PrepAndVerify())
                {
                    validScript = true;
                    error = false;
                }
                else
                {
                    validScript = false;
                    error = true;
                }
            }

            //If it's valid, let the user know in the window, and give them the option to instantiate a copy.
            if (validScript)
            {
                EditorGUILayout.HelpBox("Parse complete. No errors, check console for any warnings.", MessageType.Info, true);
                if (GUILayout.Button("Instantiate"))
                {
                    myPrefab.Instantiate();
                }
                if (GUILayout.Button("Save"))
                {
                    string savePath = EditorUtility.SaveFilePanel("t", "", "", "txt");
                    System.IO.File.WriteAllText(savePath, scriptToParse);
                }
            }

            //If there's an error, the user know.
            if (error)
            {
                EditorGUILayout.HelpBox("Error parsing! See console for errors.", MessageType.Error, true);
            }
        }
    }
}
