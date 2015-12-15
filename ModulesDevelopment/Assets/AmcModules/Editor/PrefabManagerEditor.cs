using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

public class PrefabManagerEditor : EditorWindow
{
    private static string scriptToParse = "";
    private bool validScript = false;
    private bool error = false;

    private int selGridInt = 0;
    public string[] selStrings = new string[] { "External resources", "Build custom object" };
    private AmcCustomPrefab myPrefab;
    private PrefabManager man;

    [MenuItem("AMC Tools/AMC Object Manager")]
    static void Init()
    {
        //GetWindow will either retrieve an existing window, or create a new one if one doesn't exist.
        //PrefabManagerEditor newManager = (PrefabManagerEditor)EditorWindow.GetWindow (typeof (PrefabManagerEditor));
        EditorWindow.GetWindow(typeof(PrefabManagerEditor));
    }

    void OnEnable()
    {
        if (man == null)
        {
            man = new PrefabManager();
        }
    }

    public void OnGUI()
    {
        GUILayout.BeginHorizontal("Box");
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2,GUILayout.Height(30));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical("Box");
        if (selGridInt == 1)
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
            }

            //If there's an error, the user know.
            if (error)
            {
                EditorGUILayout.HelpBox("Error parsing! See console for errors.", MessageType.Error, true);
            }
        }
        else if (selGridInt == 0)
        {
            if (GUILayout.Button("Load Resource folder"))
            {
                man.SpawnAllPrefabs();
            }
        }
        GUILayout.EndVertical();
    }
}
