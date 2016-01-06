using System;
using UnityEditor;
using UnityEngine;

namespace AmcCustomPrefab
{
    class ScriptView : IView
    {
        private CreationPresenter presenter;
        public ScriptView(CreationPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Display()
        {
            bool wrap = EditorStyles.textField.wordWrap;
            EditorStyles.textField.wordWrap = true;
            //Create a text area that fills the window
            string updatedScript = EditorGUILayout.TextArea(presenter.ScriptToParse, GUILayout.ExpandHeight(true));
            EditorStyles.textField.wordWrap = wrap;

            if (!updatedScript.Equals(presenter.ScriptToParse))
            {
                presenter.InvalidateScript();
            }

            presenter.SetScriptToParse(updatedScript);

            if (GUILayout.Button("Parse script"))
            {
                presenter.ParseScript();
            }

            //If it's valid, let the user know in the window, and give them the option to instantiate a copy.
            if (presenter.ValidScript)
            {
                EditorGUILayout.HelpBox("Parse complete. No errors, check console for any warnings.", MessageType.Info, true);
                if (GUILayout.Button("Instantiate"))
                {
                    presenter.InstantiatePrefab();
                }
                if (GUILayout.Button("Save"))
                {
                   presenter.SavePrefab(EditorUtility.SaveFilePanel("t", "", "", "txt"));
                }
            }

            //If there's an error, the user know.
            if (presenter.Error)
            {
                EditorGUILayout.HelpBox("Error parsing! See console for errors.", MessageType.Error, true);
            }
        }

        public void SetPresenter()
        {
            throw new NotImplementedException();
        }
    }
}
