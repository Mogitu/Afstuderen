using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

public class PrefabManagerEditor : EditorWindow {

	static string scriptToParse = "";
	bool validScript = false;
	bool error = false;
	AmcCustomPrefab myPrefab;
	
	[MenuItem ("AMC Tools/Prefab Manager")]
	static void Init () {
		//GetWindow will either retrieve an existing window, or create a new one if one doesn't exist.
		//PrefabManagerEditor newManager = (PrefabManagerEditor)EditorWindow.GetWindow (typeof (PrefabManagerEditor));
        EditorWindow.GetWindow(typeof(PrefabManagerEditor));
	}


	public void OnGUI () {

		bool wrap = EditorStyles.textField.wordWrap;
		EditorStyles.textField.wordWrap = true;
		//Create a text area that fills the window
		string updatedScript = EditorGUILayout.TextArea(scriptToParse, GUILayout.ExpandHeight (true));
		EditorStyles.textField.wordWrap = wrap;


		if(!updatedScript.Equals(scriptToParse)) {
			//invalidate our prefab and script when the script changes
			validScript = false;
			error = false;
		}
		scriptToParse = updatedScript;


		if(GUILayout.Button("Parse script")) {
			//Pass in the contents of the textarea as the script for this prefab
			myPrefab = new AmcCustomPrefab("TestPrefab", scriptToParse);
			if(myPrefab.PrepAndVerify()) {
				validScript = true;
				error = false;
			} else {
				validScript = false;
				error = true;
			}
		}
		
		//If it's valid, let the user know in the window, and give them the option to instantiate a copy.
		if(validScript) {
			EditorGUILayout.HelpBox("Parse complete. No errors, check console for any warnings.", MessageType.Info, true);
			if(GUILayout.Button("Instantiate")) {
				myPrefab.Instantiate();
			}
		}

		//If there's an error, the user know.
		if(error) {
			EditorGUILayout.HelpBox("Error parsing! See console for errors.", MessageType.Error, true);
		}
	}
	
}
